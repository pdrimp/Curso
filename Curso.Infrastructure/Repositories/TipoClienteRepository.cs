using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Curso.Application.Interfaces;
using Curso.Domain.Entities;
using Curso.Infrastructure.Data;

namespace Curso.Infrastructure.Repositories
{
    public class TipoClienteRepository : ITipoClienteRepository
    {
        private readonly AppDbContext _db;

        public TipoClienteRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(TipoCliente tipoCliente)
        {
            await _db.TiposClientes.AddAsync(tipoCliente);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.TiposClientes.FindAsync(id);
            if (e == null) return;
            _db.TiposClientes.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TipoCliente>> GetAllAsync()
        {
            return await _db.TiposClientes.AsNoTracking().ToListAsync();
        }

        public async Task<TipoCliente> GetByIdAsync(Guid id)
        {
            return await _db.TiposClientes.FindAsync(id);
        }

        public async Task UpdateAsync(TipoCliente tipoCliente)
        {
            _db.TiposClientes.Update(tipoCliente);
            await _db.SaveChangesAsync();
        }
    }
}
