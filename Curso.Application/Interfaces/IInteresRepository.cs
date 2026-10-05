using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curso.Domain.Entities;

namespace Curso.Application.Interfaces
{
    public interface IInteresRepository
    {
        Task AddAsync(Interes interes);
        Task<Interes> GetByIdAsync(Guid id);
        Task<List<Interes>> GetAllAsync();
        Task<List<Interes>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task UpdateAsync(Interes interes);
        Task DeleteAsync(Guid id);
    }
}
