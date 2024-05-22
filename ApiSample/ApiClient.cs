using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiSample
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://20.234.113.211:8106");
        }

        public async Task<bool> UpdateMenuAsync(int menuProductsID, MenuCategory updatedMenuCategory)
        {
            var json = JsonConvert.SerializeObject(updatedMenuCategory);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"DesktopModules/MVC/MenuModule/API/MenuApi/UpdateMenu?menuProductsID={menuProductsID}", content);

            return response.IsSuccessStatusCode;
        }

        
    }
}
