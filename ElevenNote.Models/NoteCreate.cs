using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
   public class NoteCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least two characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters.")]
        public string Title { get; set; }
        [MaxLength(8000)]
        public string Content { get; set; }
        public Categories Category { get; set; }
        public enum Categories
        {
            Poem = 1,
            Thought,
            To_Do_List,
            Grocery_List
        }
    }
}
