using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.ViewModels
{
    public class AppointmentsVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

        public string Status { get; set; }
        public string Complaint { get; set; }

        public DateTime RequestDate { get; set; }
        public string ReferenceNumber { get; set; }




    }
}
