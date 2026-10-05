using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Application.DTOs;
using Curso.Application.Interfaces;
using Curso.Domain.Entities;

namespace Curso.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IInteresRepository _interesRepository;

        public ClienteService(IClienteRepository repository, IInteresRepository interesRepository)
        {
            _repository = repository;
            _interesRepository = interesRepository;
        }

        public async Task<Guid> CrearClienteAsync(ClienteInput input)
        {
            if (await _repository.ExistsByEmailAsync(input.Correo))
                throw new InvalidOperationException("Correo repetido");

            if (string.IsNullOrWhiteSpace(input.Password)) throw new ArgumentException("Password es requerida");

            var cliente = new Cliente(input.Nombre, input.Correo, input.Password, input.TipoClienteId);

            if (input.InteresIds?.Any() == true)
            {
                var selected = await _interesRepository.GetByIdsAsync(input.InteresIds);
                cliente.SetIntereses(selected);
            }

            await _repository.AddAsync(cliente);
            return cliente.Id;
        }

        public async Task<ClienteResponse> GetClienteByIdAsync(Guid id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;
            return new ClienteResponse
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Correo = e.Correo,
                TipoClienteId = e.TipoClienteId,
                TipoClienteDescripcion = e.TipoCliente?.Descripcion,
                InteresIds = e.Intereses?.Select(i => i.Id).ToList() ?? new List<Guid>()
            };
        }

        public async Task<List<ClienteResponse>> GetAllClientesAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(e => new ClienteResponse
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Correo = e.Correo,
                TipoClienteId = e.TipoClienteId,
                TipoClienteDescripcion = e.TipoCliente?.Descripcion,
                InteresIds = e.Intereses?.Select(i => i.Id).ToList() ?? new List<Guid>()
            }).ToList();
        }

        public async Task UpdateClienteAsync(Guid id, ClienteInput input)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Cliente no encontrado");

            if (!string.Equals(existing.Correo, input.Correo, StringComparison.OrdinalIgnoreCase))
            {
                if (await _repository.ExistsByEmailAsync(input.Correo, id))
                    throw new InvalidOperationException("Correo repetido");
                existing.SetCorreo(input.Correo);
            }

            if (!string.IsNullOrWhiteSpace(input.Nombre)) existing.SetNombre(input.Nombre);
            if (!string.IsNullOrWhiteSpace(input.Password)) existing.SetPassword(input.Password);
            existing.SetTipoCliente(input.TipoClienteId);

            if (input.InteresIds != null)
            {
                var selected = await _interesRepository.GetByIdsAsync(input.InteresIds);
                existing.SetIntereses(selected);
            }

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteClienteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;
            await _repository.DeleteAsync(id);
        }
    }
}
