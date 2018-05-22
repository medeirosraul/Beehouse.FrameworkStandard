using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beehouse.FrameworkStandard.Entities
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}