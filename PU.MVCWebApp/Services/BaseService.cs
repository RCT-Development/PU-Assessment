using Newtonsoft.Json;
using PU.Core.Models;
using PU.MVCWebApp.Services.Contracts;

namespace PU.MVCWebApp.Services
{
    public class BaseService<Model> : IBaseService<Model> where Model : ModelBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public BaseService(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = baseUrl;
        }

        public IHttpClientFactory HttpClientFactory => _httpClientFactory;
        public string BaseUrl => _baseUrl;
        public async Task<HttpResponseMessage> Create(Model entity)
        {
            var client = _httpClientFactory.CreateClient();
            return await client.PostAsJsonAsync($"{_baseUrl}", entity);
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            return await client.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<List<Model>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            var result = new List<Model>();
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Model>>(await response.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<Model> GetById(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Model>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> Update(Model entity)
        {
            var client = _httpClientFactory.CreateClient();
            return await client.PutAsJsonAsync($"{_baseUrl}/{entity.Id}", entity);
        }
    }
}
