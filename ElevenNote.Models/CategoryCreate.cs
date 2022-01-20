using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryCreate
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The category name cannot be longer than 100 characters.")]
        [MinLength(3, ErrorMessage = "Category must be at least three characters long.")]
        public string Name { get; set; }
    }
}
