using NLayer.Core.DTOs;
using System.ComponentModel;

namespace NLayer.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync
                <CustomResponseDTO<List<CategoryDTO>>>("Categories");
            return response.Data;
        }
    }
}
