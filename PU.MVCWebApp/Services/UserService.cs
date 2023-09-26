using Newtonsoft.Json;
using PU.Core.Models;
using PU.MVCWebApp.Services.Contracts;

namespace PU.MVCWebApp.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "https://localhost:7059/api/User")
        { 
        }

        public async Task<int> GetUserCount()
        {
            var client = HttpClientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}/Count");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            }

            return 0;
        }
    }
}
