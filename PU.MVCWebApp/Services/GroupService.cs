using Newtonsoft.Json;
using PU.Core.Models;
using PU.MVCWebApp.Services.Contracts;
using System.Net.Http;

namespace PU.MVCWebApp.Services
{
    public class GroupService : BaseService<Group>, IGroupService
    {

        public GroupService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "https://localhost:7059/api/Group")
        {
        }

        public async Task AddPermissionToGroup(Guid permissionId, Guid groupId)
        {
            var client = HttpClientFactory.CreateClient();
            await client.PostAsync($"{BaseUrl}/{groupId}/Permissions/{permissionId}", new StringContent(string.Empty));
        }

        public async Task RemovePermissionFromGroup(Guid permissionId, Guid groupId)
        {
            var client = HttpClientFactory.CreateClient();
            await client.DeleteAsync($"{BaseUrl}/{groupId}/Permissions/{permissionId}");
        }

        public async Task AddUserToGroup(Guid userId, Guid groupId)
        {
            var client = HttpClientFactory.CreateClient();
            await client.PostAsync($"{BaseUrl}/{groupId}/Users/{userId}", new StringContent(string.Empty));
        }

        public async Task RemoveUserFromGroup(Guid userId, Guid groupId)
        {
            var client = HttpClientFactory.CreateClient();
            await client.DeleteAsync($"{BaseUrl}/{groupId}/Users/{userId}");
        }

        public async Task<List<Permission>> GetGroupPermissions(Guid id)
        {
            var client = HttpClientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}/{id}/Permissions");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Permission>>(await response.Content.ReadAsStringAsync()); ;
        }

        public async Task<List<User>> GetGroupUsers(Guid id)
        {
            var client = HttpClientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}/{id}/Users");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync()); ;
        }
        
    }
}
