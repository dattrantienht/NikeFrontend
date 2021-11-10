using Blazored.SessionStorage;
using NikeFrontend.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class ProductCategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionStorageService _sessionStorageService;

        public ProductCategoryService(IHttpClientFactory ClientFactory,
            ISessionStorageService SessionStorageService)
        {
            _clientFactory = ClientFactory;
            _sessionStorageService = SessionStorageService;
        }

        public async Task<ProductCategoryModelRootobject> getListProductCategory()
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<ProductCategoryModelRootobject>("ProductCategories");
            return await Task.FromResult(result);
        }

        public async Task<SingleProductCategoryModelRootobject> getProductCategory(int id)
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<SingleProductCategoryModelRootobject>($"ProductCategories/{id}");
            return await Task.FromResult(result);
        }

        public async Task<HttpResponseMessage> addProductCategory(ProductCategoryModel productCategory)
        {
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            var client = _clientFactory.CreateClient("KSC");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var result = await client.PostAsJsonAsync("ProductCategories", productCategory);
            return await Task.FromResult(result);
        }
    }
}