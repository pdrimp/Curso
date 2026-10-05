using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curso.Application.DTOs;

namespace Curso.Application.Interfaces
{
    public interface IInteresService
    {
        Task<Guid> CrearInteresAsync(InteresInput input);
        Task<InteresResponse> GetInteresByIdAsync(Guid id);
        Task<List<InteresResponse>> GetAllInteresesAsync();
        Task UpdateInteresAsync(Guid id, InteresInput input);
        Task DeleteInteresAsync(Guid id);
    }
}
