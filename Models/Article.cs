using System;
using System.ComponentModel.DataAnnotations;

namespace Micro_social_platform.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "The maximum length must be 100 characters")]
        [MinLength(5, ErrorMessage = "The minimum length must be 5 characters")]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Article can't be empty")]
        [MinLength(15, ErrorMessage = "The minimum length must be 15 characters")]
        public string Content { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }// un articol apartine unui singur utilizator

        public virtual ICollection<Comment>? Comments { get; set; }

    }
}

