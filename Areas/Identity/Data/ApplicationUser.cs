using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the RazorPagesMovieUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [Display(Name = "Date of Birth")]
        [PersonalData]
        public DateTime DOB { get; set; }

    }
}
