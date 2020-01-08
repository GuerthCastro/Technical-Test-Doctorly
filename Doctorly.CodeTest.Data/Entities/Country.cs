using System;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.CodeTest.Data.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}