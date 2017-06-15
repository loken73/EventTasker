using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventTasker.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual List<ToDo> ToDos { get; set; }

        public ApplicationUser()
        {
            this.ToDos = new List<ToDo>();
        }
    }
}
