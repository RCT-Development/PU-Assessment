using PU.Core.DTO.Response;
using PU.Core.Models;

namespace PU.MVCWebApp.Models.GroupViewModels
{
    public class GroupViewModel
    {
        public List<Group> Groups { get; set; }
        public List<GroupUserCount> GroupUserCounts { get; set; }
    }
}
