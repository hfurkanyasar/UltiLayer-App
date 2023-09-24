using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDTO:IServiceWithDTO<Product ,ProductDTO>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory();

        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO dto);

        Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDTO dto);

        Task<CustomResponseDTO<List<ProductDTO>>> AddRangeAsync(ProductCreateDTO dto);

    }
}
