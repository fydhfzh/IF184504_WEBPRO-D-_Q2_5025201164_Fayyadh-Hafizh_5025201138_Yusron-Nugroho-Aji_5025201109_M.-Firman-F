
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(10)]
        [DisplayName("My Diary")]
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public Note()
        {

        }
    }
}