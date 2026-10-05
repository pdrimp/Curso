using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curso.Domain.Entities;

namespace Curso.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task AddAsync(Cliente cliente);
        Task<Cliente> GetByIdAsync(Guid id);
        Task<List<Cliente>> GetAllAsync();
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Guid id);
        Task<Cliente> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null);
    }
}
