using System.ComponentModel.DataAnnotations;
using Blog.Data.Models.Abstract;

namespace Blog.Data.Models.Concrete
{
    public abstract class CoreEntity : ICoreEntity
    {
        [Key]
        public Guid Id { get; set; }

        public CoreEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}