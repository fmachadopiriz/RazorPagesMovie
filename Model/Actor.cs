using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Actor : Person
    {
        public bool AwardedBestActor { get; set; }
    }
}