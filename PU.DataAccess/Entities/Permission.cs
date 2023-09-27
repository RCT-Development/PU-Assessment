using System.ComponentModel.DataAnnotations;

namespace PU.DataAccess.Entities
{
    public class Permission : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
