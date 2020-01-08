using System;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.CodeTest.Data.Entities
{
    public class RecipeVersion
    {
        [Key]
        public int RecipeVersionId { get; set; }
        [Required]
        public int RecipeId { get; set; }
        [Required]
        public int VersionNumber { get; set; }
        [Required]
        public string Preparation { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}