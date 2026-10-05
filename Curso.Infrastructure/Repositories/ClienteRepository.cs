using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Curso.Application.Interfaces;
using Curso.Domain.Entities;
using Curso.Infrastructure.Data;

namespace Curso.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _db;

        public ClienteRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _db.Clientes.AddAsync(cliente);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Clientes.FindAsync(id);
            if (e == null) return;
            _db.Clientes.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _db.Clientes.Include(c => c.TipoCliente).Include(c => c.Intereses).AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            var key = email.Trim().ToLowerInvariant();
            return await _db.Clientes.Include(c => c.TipoCliente).Include(c => c.Intereses).FirstOrDefaultAsync(c => c.Correo == key);
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            return await _db.Clientes.Include(c => c.TipoCliente).Include(c => c.Intereses).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _db.Clientes.Update(cliente);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var key = email.Trim().ToLowerInvariant();
            var q = _db.Clientes.AsQueryable().Where(c => c.Correo == key);
            if (excludeId.HasValue) q = q.Where(c => c.Id != excludeId.Value);
            return await q.AnyAsync();
        }
    }
}
