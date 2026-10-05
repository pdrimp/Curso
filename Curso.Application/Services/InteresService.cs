using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Application.DTOs;
using Curso.Application.Interfaces;
using Curso.Domain.Entities;

namespace Curso.Application.Services
{
    public class InteresService : IInteresService
    {
        private readonly IInteresRepository _repository;

        public InteresService(IInteresRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CrearInteresAsync(InteresInput input)
        {
            var entity = new Interes(input.Descripcion);
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<InteresResponse> GetInteresByIdAsync(Guid id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;
            return new InteresResponse { Id = e.Id, Descripcion = e.Descripcion };
        }

        public async Task<List<InteresResponse>> GetAllInteresesAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(e => new InteresResponse { Id = e.Id, Descripcion = e.Descripcion }).ToList();
        }

        public async Task UpdateInteresAsync(Guid id, InteresInput input)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Interes no encontrado");
            existing.SetDescripcion(input.Descripcion);
            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteInteresAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;
            await _repository.DeleteAsync(id);
        }
    }
}
