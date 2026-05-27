using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

    }
}
