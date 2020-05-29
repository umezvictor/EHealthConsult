using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Models
{
    public class AppUser : IdentityUser
    {
        //
        //this class extends identity user table
        //it addds custom fields to it
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
