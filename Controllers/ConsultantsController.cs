using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using EHealthConsult.Models;
using EHealthConsult.Repository.Interface;
using EHealthConsult.ViewModels;
using Microsoft.AspNetCore.Mvc;

//localhost:5000/api/consultants/
namespace EHealthConsult.Controllers
{
    [Route("api/[controller]")]
    public class ConsultantsController : ControllerBase
    {

        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;


        public ConsultantsController(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }

        //end points
        //get,   post,   put,     delete, patch -- crud
        // read create, update    delete
        [HttpGet]
        public async Task<IActionResult> GetConsultants()
        {
            try
            {
                var consultants = await _repoWrapper.Consultants.GetAllConsultantsAsync();

                if (consultants == null)
                {
                    return NotFound(new { message = "no record found" });
                }
                else
                {
                    return Ok(consultants);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultant(int id)
        {
            try
            {
                var consultant = await _repoWrapper.Consultants.GetConsultantByIdAsync(id);

                if (consultant == null)
                {
                    return NotFound(new { message = "no record found" });
                }
                else
                {
                    return Ok(consultant);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateConsultant([FromForm]ConsultantsVM consultant)
        {
            try
            {
                //check if profile picture is present
                var profilePicture = consultant.ProfilePicture;
                if (profilePicture.Length <= 0) return BadRequest(new { errorMessage = "Profile picture must be uploaded" });



                //check if customer already exists

                var existingConsultant = await _repoWrapper.Consultants.GetConsultantByEmailAsync(consultant.Email);

                if (existingConsultant != null)
                {
                    return BadRequest(new { errorMessage = "Record already exists" });
                }

                //check if customer object is null
                if (consultant == null)
                {
                    return BadRequest(new { errorMessage = "consultant object can not be null" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Invalid consultant model" });
                }



                //give the file a unique id
                Guid uniqueFileId = Guid.NewGuid();

                //give file a unique name to prevent overwriting
                var uniqueFileName = uniqueFileId + profilePicture.FileName.Replace(" ", "_");//replace spaces with underscore

                //specify folder to save uploaded files
                var filePath = Path.Combine("Uploads/Images", uniqueFileName);

                //this works, but saves file inside server, not a specific folder
                // using(var fileStream = new FileStream(profilePicture.FileName, FileMode.Create))

                //save profile picture
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(fileStream);
                }

                //map incoming customer object of CustomerCreationDto to the Customer model class
                var consultantEntity = _mapper.Map<Consultant>(consultant);


                //add the unique filename to customerentity object and save to db
                consultantEntity.ProfilePicture = uniqueFileName;

                _repoWrapper.Consultants.CreateConsultant(consultantEntity);
                await _repoWrapper.SaveAsync();

                return Created($"/api/consultants/{consultantEntity.Id}", consultantEntity);
            }
            catch (Exception)
            {
                //if an exception occurs
                return this.StatusCode(501);
            }
        }

       // [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsultant(int Id, [FromBody]ConsultantsVM consultant)
        {
            try

            {

                if (consultant == null)
                {
                    return BadRequest(new { errorMessage = "object cannot be null" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Invalid object" });
                }

                //get the customer's record
                var existingRecord = await _repoWrapper.Consultants.GetConsultantByIdAsync(Id);

                if (existingRecord == null)
                {
                    return NotFound();
                }


                //map the incoming data to the data fetched from the db

                _mapper.Map(consultant, existingRecord);

                _repoWrapper.Consultants.UpdateConsultant(existingRecord);

                await _repoWrapper.SaveAsync();

                return NoContent();//success

            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }


        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultant(int id)
        {
            try
            {
                //find the consultant by id
                var consultant = await _repoWrapper.Consultants.GetConsultantByIdAsync(id);

                if (consultant == null)
                {
                    return NotFound(new { errorMessage = "Consultant does not exist" });
                }

                _repoWrapper.Consultants.DeleteConsultant(consultant);

                await _repoWrapper.SaveAsync();

                return Ok(new { successMessage = "Consultant has been successfully deleted" });
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }


        //upload profile pics via separate api
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateProfilePicture(int Id, [FromForm]UpdateProfilePicsVM profilePicsVM)
        {
            try
            {
                //get incoming picture and file name
           
                var profilePicture = profilePicsVM.ProfilePicture;

                if (profilePicture.Length <= 0) return BadRequest("Profile picture is required");

                //get the consultant's record from db using the id
                var consultant = await _repoWrapper.Consultants.GetConsultantByIdAsync(Id);

                if (consultant == null) return BadRequest("Consultant does not exist");


                //delete existing profile picture of consultant
                string oldProfilePicsName = consultant.ProfilePicture;

                if(System.IO.File.Exists(Path.Combine("Uploads/Images", oldProfilePicsName)))
                {
                    //delete file if it exists
                    System.IO.File.Delete(Path.Combine("Uploads/Images", oldProfilePicsName));
                }

                Guid uniqueFileId = Guid.NewGuid();

                var uniqueFileName = uniqueFileId + profilePicture.FileName.Replace(" ", "_");
          
                var filePath = Path.Combine("Uploads/Images", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(fileStream);
                }

                consultant.ProfilePicture = uniqueFileName;

                _repoWrapper.Consultants.UpdateConsultant(consultant);

                await _repoWrapper.SaveAsync();

                return Ok(new { successMessage = "Profile picture was updated succcesfully" });

            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
           
        }

        
        //uncle bob
        [HttpGet("getimage/{id}")]
        //[HttpGet("getimage")]
        public async Task<IActionResult> GetConsultantImage(int Id)
        {
            try
            {
                string fileExtension = "";
                var consultant = await _repoWrapper.Consultants.GetConsultantByIdAsync(Id);
                if(consultant == null)
                {
                    return NotFound(new { message = "No record found" });
                }
                //get file name of profile picture 
                string profilePictureFileName = consultant.ProfilePicture;
                //image path
                string imagePath = Path.Combine("Uploads/Images", profilePictureFileName);
                //file extension
                string extension = Path.GetExtension(imagePath).TrimStart('.');

                //note, only 'jpeg' and 'png' extensions can be rendered, 'jpg' will return gibberish characters

                if(extension == "jpeg")
                {
                    fileExtension = "jpeg";
                }
                if(extension == "jpg")
                {
                    fileExtension = "jpeg";
                }
                if(extension == "png")
                {
                    fileExtension = "png";
                }

                Byte[] b = System.IO.File.ReadAllBytes(imagePath);   
                // You can use your own method over here.         
                return File(b, "image/"+fileExtension);
                //return File(b, "image/jpeg");
                //use video/mp4 for video files
                //returns a blob
                //concat extension
                //use blob parser on frontend, 
                //browser can't read blob

            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }

        }

    }
}

