using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ServiceWithDTO<Entity, DTO> : IServiceWithDTO<Entity, DTO>
        where Entity : BaseEntity where DTO : class
    {

        private readonly IGenericRepository<Entity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceWithDTO(IGenericRepository<Entity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<DTO>> AddAsync(DTO dto)
        {
            Entity newEntity = _mapper.Map<Entity>(dto);

            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDTO = _mapper.Map<DTO>(newEntity);
            return CustomResponseDTO<DTO>.Succes(StatusCodes.Status200OK, newDTO);

        }

        public async Task<CustomResponseDTO<IEnumerable<DTO>>> AddRangeAsync(IEnumerable<DTO> dto)
        {
            var newEntities = _mapper.Map<IEnumerable<Entity>>(dto);

            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();

            var newDTOs = _mapper.Map<IEnumerable<DTO>>(newEntities);
            return CustomResponseDTO<IEnumerable<DTO>>.Succes(StatusCodes.Status200OK, newDTOs);
        }

        public async Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Entity, bool>> expresion)
        {
            var anyEntity = await _repository.AnyAsync(expresion);
            return CustomResponseDTO<bool>.Succes(StatusCodes.Status200OK, anyEntity);
        }

        public async Task<CustomResponseDTO<IEnumerable<DTO>>> GetAllAsync()
        {
            var entites = await _repository.GetAll().ToListAsync();
            var dtos = _mapper.Map<IEnumerable<DTO>>(entites);

            return CustomResponseDTO<IEnumerable<DTO>>.Succes(StatusCodes.Status200OK, dtos);
        }

        public async Task<CustomResponseDTO<DTO>> GetByIDAsync(int id)
        {
            var entities = await _repository.GetByIDAsync(id);
            var dtos = _mapper.Map<DTO>(entities);

            return CustomResponseDTO<DTO>.Succes(StatusCodes.Status200OK, dtos);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIDAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Succes(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> id)
        {
            var enttity = await _repository.Where(a => id.Contains(a.ID)).ToListAsync();

            _repository.RemoveRange(enttity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Succes(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(DTO dto)
        {
            var entites =_mapper.Map<Entity>(dto);
            _repository.Update(entites);
            await _unitOfWork.CommitAsync();

            return CustomResponseDTO<NoContentDTO>.Succes(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<IEnumerable<DTO>>> Where(Expression<Func<Entity, bool>> expresion)
        {
            var  entities = await _repository.Where(expresion).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<DTO>>(entities);

            return CustomResponseDTO<IEnumerable<DTO>>.Succes(StatusCodes.Status200OK, dtos);
        }
    }
}
