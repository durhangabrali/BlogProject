using System.ComponentModel.DataAnnotations;
 
namespace Blog.Data.Models.Concrete
{  
    public class Category : CoreEntity
    {        
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Post>? Posts { get;set;}
    }
}