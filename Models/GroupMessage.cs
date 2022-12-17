using System.ComponentModel.DataAnnotations;

namespace Micro_social_platform.Models
{
    public class GroupMessage
    {
        [Key]
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Continutul este obligatoriu")]  
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }




    }
}
