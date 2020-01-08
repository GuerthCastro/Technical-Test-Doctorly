using System;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.CodeTest.Data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int CountryId{ get; set; }
    }
}