using Newtonsoft.Json;
using PU.Core.Models;
using PU.MVCWebApp.Services.Contracts;
using System.Net.Http;

namespace PU.MVCWebApp.Services
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        public PermissionService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "https://localhost:7059/api/Permission")
        {
            
        }
    }
}
