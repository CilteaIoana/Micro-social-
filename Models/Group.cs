using System.ComponentModel.DataAnnotations;


namespace Micro_social_platform.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Denumirea grupului este obligatorie")]
        public string Name { get; set; }        
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }


    }
}
