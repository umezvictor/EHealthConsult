using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.ViewModels
{
    public class ConsultantsVM
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        //a short write up about the consultant
        public int YearsOfExperience { get; set; }
        public string Phone { get; set; }

        public string Profession { get; set; }
        public string ProfilePicture { get; set; }
        public string ZoomId { get; set; }
       public string SkypeId { get; set; }
    }
}
