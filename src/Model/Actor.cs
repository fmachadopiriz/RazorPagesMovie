using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RazorPagesMovie.Models
{
    public class Actor : Person
    {
        [Display(Name = "Awarded Best Actor")]
        public bool AwardedBestActor { get; set; }

        public List<Appereance> Appereances { get; set; }
    }
}