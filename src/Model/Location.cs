using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RazorPagesMovie.Models
{
    public class Location
    {
        [Key]
        public int MovieID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Location")]
        public string Name { get; set; }

        public Movie Movie { get; set; }
    }
}