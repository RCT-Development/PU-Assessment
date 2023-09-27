using System.ComponentModel.DataAnnotations;

namespace PU.DataAccess.Entities
{
    public class Group : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
