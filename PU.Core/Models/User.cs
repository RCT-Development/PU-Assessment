using System.ComponentModel.DataAnnotations;

namespace PU.Core.Models
{
    public class User : ModelBase
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactNumber { get; set; }
    }
}
