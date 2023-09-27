using System.ComponentModel.DataAnnotations;

namespace PU.DataAccess.Entities
{
    public class User : EntityBase
    {
        public Guid UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactNumber { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
