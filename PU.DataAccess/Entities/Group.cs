using System.ComponentModel.DataAnnotations;

namespace PU.DataAccess.Entities
{
    public class Group : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
