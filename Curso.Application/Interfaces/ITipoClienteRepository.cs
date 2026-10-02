using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curso.Domain.Entities;

namespace Curso.Application.Interfaces
{
    public interface ITipoClienteRepository
    {
        Task AddAsync(TipoCliente tipoCliente);
        Task<TipoCliente> GetByIdAsync(Guid id);
        Task<List<TipoCliente>> GetAllAsync();
        Task UpdateAsync(TipoCliente tipoCliente);
        Task DeleteAsync(Guid id);
    }
}
