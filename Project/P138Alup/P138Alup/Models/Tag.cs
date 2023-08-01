using System.ComponentModel.DataAnnotations;

namespace P138Alup.Models
{
    public class Tag : BaseEntity
    {
        [StringLength(255)]
        public string Name { get; set; }
    }
}
