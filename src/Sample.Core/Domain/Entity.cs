using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Core.Domain
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public void SoftDelete()
        {
            IsDeleted = true;
        }

        public void UnDelete()
        {
            IsDeleted = false;
        }
    }
}
