using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IServiceWithDTO<Entity, DTO> where Entity : BaseEntity where DTO : class
    {
        Task<CustomResponseDTO<DTO>> GetByIDAsync(int id);
        Task<CustomResponseDTO<IEnumerable<DTO>>> GetAllAsync();

        //queryable olması sorgunun kullanılırken orderby tolist vs. kullanımı olması için
        // taki enumarable komutu verilene kadar(tolistasync) bekler.
        Task<CustomResponseDTO<IEnumerable<DTO>>> Where(Expression<Func<Entity, bool>> expresion);

        Task<CustomResponseDTO< bool>> AnyAsync(Expression<Func<Entity, bool>> expresion);

        Task<CustomResponseDTO<DTO>> AddAsync(DTO dto);

        Task<CustomResponseDTO<IEnumerable<DTO>>> AddRangeAsync(IEnumerable<DTO> dto);

        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(DTO dto);

        Task<CustomResponseDTO<NoContentDTO>> RemoveAsync( int id);

        Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> id);
    }
}
