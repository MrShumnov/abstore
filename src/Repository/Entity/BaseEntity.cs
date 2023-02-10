using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime LastChangeTime { get; set; }
    }
}
