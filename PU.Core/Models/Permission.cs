using System.ComponentModel.DataAnnotations;

namespace PU.Core.Models
{
    public class Permission : ModelBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
