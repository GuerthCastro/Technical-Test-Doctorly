using System;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.CodeTest.Data.Entities
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        [Required]
        public string RecipeName { get; set; }
        [Required]
        public int CreateBy { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }

    }
}