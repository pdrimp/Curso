using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curso.Application.DTOs;

namespace Curso.Application.Interfaces
{
    public interface IClienteService
    {
        Task<Guid> CrearClienteAsync(ClienteInput input);
        Task<ClienteResponse> GetClienteByIdAsync(Guid id);
        Task<List<ClienteResponse>> GetAllClientesAsync();
        Task UpdateClienteAsync(Guid id, ClienteInput input);
        Task DeleteClienteAsync(Guid id);
    }
}
