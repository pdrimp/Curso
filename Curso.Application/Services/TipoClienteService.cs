using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Application.DTOs;
using Curso.Application.Interfaces;
using Curso.Domain.Entities;

namespace Curso.Application.Services
{
    public class TipoClienteService : ITipoClienteService
    {
        private readonly ITipoClienteRepository _repository;

        public TipoClienteService(ITipoClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CrearTipoClienteAsync(TipoClienteInput input)
        {
            var entity = new TipoCliente(input.Descripcion);
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<TipoClienteResponse> GetTipoClienteByIdAsync(Guid id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;
            return new TipoClienteResponse { Id = e.Id, Descripcion = e.Descripcion };
        }

        public async Task<List<TipoClienteResponse>> GetAllTiposClientesAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(e => new TipoClienteResponse { Id = e.Id, Descripcion = e.Descripcion }).ToList();
        }

        public async Task UpdateTipoClienteAsync(Guid id, TipoClienteInput input)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("TipoCliente no encontrado");
            existing.SetDescripcion(input.Descripcion);
            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteTipoClienteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return; // idempotent
            await _repository.DeleteAsync(id);
        }
    }
}
