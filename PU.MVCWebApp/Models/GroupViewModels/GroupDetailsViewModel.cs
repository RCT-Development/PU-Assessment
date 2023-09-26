using PU.Core.Models;

namespace PU.MVCWebApp.Models.GroupViewModels
{
    public class GroupDetailsViewModel
    {
        public Group Group { get; set; }
        public List<User> Users { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
