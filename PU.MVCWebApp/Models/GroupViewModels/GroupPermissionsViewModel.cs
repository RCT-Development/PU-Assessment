using PU.Core.Models;

namespace PU.MVCWebApp.Models.GroupViewModels
{
    public class GroupPermissionsViewModel
    {
        public Guid GroupId { get; set; }
        public List<Permission> AvailablePermissions { get; set; } = new List<Permission>();
        public List<Permission> GroupPermissions { get; set; } = new List<Permission>();
    }
}
