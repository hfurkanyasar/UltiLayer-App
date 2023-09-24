using NLayer.Core.DTOs;
using System.Net.Http.Json;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;
        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDTO>> GetProductsWithCategoryAsync()
        {
            // getformjsyon kendi jsona cast ediyor.
            var response = await _httpClient.GetFromJsonAsync
                <CustomResponseDTO<List<ProductWithCategoryDTO>>>("Products/GetProductWithCategory");
            return response.Data;

        }
        public async Task<ProductDTO> SaveAsync(ProductDTO newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("Products", newProduct);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductDTO>>();
            return responseBody.Data;
          
        }
        public async Task<bool> UpdateAsync(ProductDTO newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("Products", newProduct);


            return response.IsSuccessStatusCode;

        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Products/{id}");

            return response.IsSuccessStatusCode;

        }
        public async Task<ProductDTO> GetByIDAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<ProductDTO>>($"Products/{id}");

            #region  Olabilir
            // yapılabilecek bir senaryo..
            //if (response.Errors.Any())
            //{
            //    //logla,hata fırlat.
            //}
            //else
            //{
            //    //datayı dön...
            //} 
            #endregion

            return response.Data;
        
        }

    }
}
