using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Models
{
    public class Appointment
    {
        //
        [Key]
        public int Id { get; set; } //patiednt id
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Problem { get; set; }
      
        public string Remark { get; set; }

  
    }
}
