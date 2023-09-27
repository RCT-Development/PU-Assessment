using PU.Core.Models;

namespace PU.MVCWebApp.Models.GroupViewModels
{
    public class GroupUserViewModel
    {
        public Guid GroupId { get; set; }
        public List<User> AvailableUsers { get; set; } = new List<User>();
        public List<User> GroupUsers { get; set; } = new List<User>();
    }
}
