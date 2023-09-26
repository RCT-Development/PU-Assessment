using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PU.Core.Models
{
    public class Group : ModelBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
