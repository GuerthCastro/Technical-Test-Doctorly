using System;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.CodeTest.Repository.DTOS
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(150)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(150)]
        public string LastName { get; set; }
    }
}
