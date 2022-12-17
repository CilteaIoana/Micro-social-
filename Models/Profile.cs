using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro_social_platform.Models
    {
        public class Profile
        {
            [Key]

            public int? ProfileId { get; set; }
            [Required(ErrorMessage = "First name required")]
            [MaxLength(20, ErrorMessage = "Can't have more than 20 characters")]
            public string? FirstName { get; set; }
            [Required(ErrorMessage = "Last name required")]
            [MaxLength(20, ErrorMessage = "Can't have more than 20 characters")]
            public string? LastName { get; set; }
            [Required(ErrorMessage = "Avatar is required")]
            public string? ProfilePicture { get; set; }
            [Required(ErrorMessage = "User Name required")]
            [MaxLength(20, ErrorMessage = "Can't have more than 20 characters")]
            public string? UserName { get; set; }

            [Required(ErrorMessage = "Description required")]
            public string? Description { get; set; }
            [Required(ErrorMessage = "Visibility required")]
            public bool? IsPrivate { get; set; }
            public DateTime JoinDate { get; set; }
            public string? UserId { get; set; }
            public virtual ApplicationUser User { get; set; } // un profil apartine unui singur utilizator

        }
}
