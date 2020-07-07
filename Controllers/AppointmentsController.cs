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
using EHealthConsult.Services.Interfaces;
using EHealthConsult.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EHealthConsult.Controllers
{
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {

        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AppointmentsController(IRepositoryWrapper repoWrapper, IMapper mapper, IEmailService emailService)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _emailService = emailService;
        }


        [HttpGet]
        public async Task<IActionResult> GetActiveAppointments()
        {
            try
            {
                bool isDeletedStatus = false;

                var appointments = await _repoWrapper.Appointments.GetAllAppointmentsByDeleteStatus(isDeletedStatus);

                if (appointments == null)
                {
                    return NotFound(new { message = "no record found" });
                }
                else
                {
                    return Ok(appointments);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var appointments = await _repoWrapper.Appointments.GetAllAppointmentsAsync();

                if (appointments == null)
                {
                    return NotFound(new { message = "no record found" });
                }
                else
                {
                    return Ok(appointments);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{referenceNumber}")]
        public async Task<IActionResult> GetAppointmentByReference(string referenceNumber)
        {
            try
            {
                var appointment = await _repoWrapper.Appointments.GetAppointmentByReferenceAsync(referenceNumber);

                if (appointment == null)
                {
                    return NotFound(new { message = "no record found" });
                }
                else
                {
                    return Ok(appointment);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> CreateAppointment(int id, [FromBody]AppointmentsVM appointment)
        {
            try
            {
                
                              
                if (appointment == null)
                {
                    return BadRequest(new { errorMessage = "object can not be null" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Invalid model" });
                }

                //takes in consultantid -
                //find the consultant which the client is requesting an appointment with
                //send email to the consultant
                var consultant = await _repoWrapper.Consultants.GetConsultantByIdAsync(id);
                if (consultant == null) return BadRequest(new { message = "no matching consultant record found" });

                //get consultant email
                string consultantEmail = consultant.Email;
                //build email 
                //string subject = "Request for Appointment";
                //string messageToConsultant = $"{appointment.FirstName}  {appointment.LastName} has requested to have an appointment with you";

               

                //generate random reference number for each appointment
                appointment.ReferenceNumber = GenerateRandomNumber(10);
                appointment.RequestDate = DateTime.Now;
                appointment.Status = "pending";

                //message body for patient -- 
                //string messageToPatient = $"Your request has been received, {consultant.FirstName} {consultant.LastName} will contact you shortly. " +
                    //$"your reference number is {appointment.ReferenceNumber}";


                var newAppointment = _mapper.Map<Appointment>(appointment);

                //set appointment delete status to false
                newAppointment.isDeleted = false;

                _repoWrapper.Appointments.CreateAppointment(newAppointment);
                await _repoWrapper.SaveAsync();

                //send email to consultant
                //_emailService.SendEmail(consultantEmail, subject, messageToConsultant);

                //send mail to client
                //_emailService.SendEmail(appointment.Email, subject, messageToPatient);
               
                return Created($"/api/appointments/{newAppointment.Id}", newAppointment);

            }
            catch (Exception)
            {
                //if an exception occurs
                return this.StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int Id, [FromBody]AppointmentsVM appointment)
        {
            try

            {

                if (appointment == null)
                {
                    return BadRequest(new { errorMessage = "object cannot be null" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Invalid object" });
                }

                //get the customer's record
                var existingRecord = await _repoWrapper.Appointments.GetAppointmentByIdAsync(Id);

                if (existingRecord == null)
                {
                    return NotFound();
                }


                //map the incoming data to the data fetched from the db

                _mapper.Map(appointment, existingRecord);

                _repoWrapper.Appointments.UpdateAppointment(existingRecord);

                await _repoWrapper.SaveAsync();

                return NoContent();//success

            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }


        }


        [HttpDelete("{referenceNumber}")]
        public async Task<IActionResult> DeleteAppointment(string referenceNumber)
        {
            try
            {
                //find the consultant by id
                var appointment = await _repoWrapper.Appointments.GetAppointmentByReferenceAsync(referenceNumber);

                if (appointment == null)
                {
                    return NotFound(new { errorMessage = "appointment does not exist" });
                }

                //set appointment delete status to true
                appointment.isDeleted = true;

                _repoWrapper.Appointments.DeleteAppointment(appointment);

                await _repoWrapper.SaveAsync();

                return Ok(new { successMessage = "appointment has been successfully deleted" });
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }


        [HttpPut("updateStatus/{refNumber}")]
        public async Task<IActionResult> UpdateAppointmentStatus(string refNumber, [FromBody]UpdateAppointmentStatusVM appointmentVM)
        {
            try

            {

                if (appointmentVM == null)
                {
                    return BadRequest(new { errorMessage = "object cannot be null" });
                }   
                
                //get the customer's record
                var appointmentDetails = await _repoWrapper.Appointments.GetAppointmentByReferenceAsync(refNumber);

                if (appointmentDetails == null)
                {
                    return NotFound();
                }

                //get apointment status
                if (appointmentVM.Status == "inprogress")
                {
                    appointmentDetails.Status = "inprogress";
                }
                else if(appointmentVM.Status == "closed")
                {
                    appointmentDetails.Status = "closed";
                }
                else
                {
                    appointmentDetails.Status = "pending";
                }
               
                _repoWrapper.Appointments.UpdateAppointment(appointmentDetails);

                await _repoWrapper.SaveAsync();

                return NoContent();//success

            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }


        }

        //method to generate random number
        public string GenerateRandomNumber(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }


        [HttpPost("sendmail")]
        public IActionResult SendMail([FromBody] EmailVM emailVM)
        {
            try
            {    
                if(emailVM.Email == null) { return BadRequest(new { message = "object cannot be null" }); }
                
                 _emailService.SendEmail(emailVM.Email, emailVM.Subject, emailVM.Body);
               
                return Ok(new { message = "mail sent successfully"});
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
            
        }




        /*
          [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                //find the consultant by id
                var appointment = await _repoWrapper.Appointments.GetAppointmentByIdAsync(id);

                if (appointment == null)
                {
                    return NotFound(new { errorMessage = "appointment does not exist" });
                }

                _repoWrapper.Appointments.DeleteAppointment(appointment);

                await _repoWrapper.SaveAsync();

                return Ok(new { successMessage = "appointment has been successfully deleted" });
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }

         */
    }
}

