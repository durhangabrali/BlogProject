using System.ComponentModel.DataAnnotations;
using Blog.Data.Models.Abstract;

namespace Blog.Data.Models.Concrete
{
    public abstract class CoreEntity : ICoreEntity
    {
        public CoreEntity()
        {
            this.Id = Guid.NewGuid();
        }
        
        [Key]
        public Guid Id { get; set; }

        
    }
}