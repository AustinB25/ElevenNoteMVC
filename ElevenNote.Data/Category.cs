using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, Display(Name = "Category")]
        public string Name { get; set; }  
        [Required]
        public Guid OwnerId { get; set; }
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public int NotesInCatergory  {get { return NumberOfNotes(Notes); } }       
        private int NumberOfNotes(ICollection<Note> notes)
        {
            int totalNotes = 0;
            foreach (var n in notes)
            {
                totalNotes += 1;
            }
            return totalNotes;
        }
    }
}
