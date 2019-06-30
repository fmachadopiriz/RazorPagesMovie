using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RazorPagesMovie.Models
{
    public class Appereance
    {
        [Key]
        public int ActorID { get; set; }

        [Key]
        public int MovieID { get; set; }

        [Required]
        public Actor Actor { get; set; }

        [Required]
        public Movie Movie { get; set; }
    }
}