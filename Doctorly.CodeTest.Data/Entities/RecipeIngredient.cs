using System;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.CodeTest.Data.Entities
{
    public class RecipeIngredient
    {
        [Key]
        public int RecipeIngredientId{ get; set; }
        [Required]
        public int RecipeVersionId { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [Required]
        public String Specification { get; set; }
    }
}