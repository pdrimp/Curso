using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curso.Application.DTOs;

namespace Curso.Application.Interfaces
{
    public interface ITipoClienteService
    {
        Task<Guid> CrearTipoClienteAsync(TipoClienteInput input);
        Task<TipoClienteResponse> GetTipoClienteByIdAsync(Guid id);
        Task<List<TipoClienteResponse>> GetAllTiposClientesAsync();
        Task UpdateTipoClienteAsync(Guid id, TipoClienteInput input);
        Task DeleteTipoClienteAsync(Guid id);
    }
}
