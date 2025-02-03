using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models.Concrete
{
    public class Post : CoreEntity
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public string FullName { get; set; }

        //navigation property
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<PostImage>? PostImages { get; set; }
    }
}