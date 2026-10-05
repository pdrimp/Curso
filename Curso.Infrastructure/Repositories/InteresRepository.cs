using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Curso.Application.Interfaces;
using Curso.Domain.Entities;
using Curso.Infrastructure.Data;

namespace Curso.Infrastructure.Repositories
{
    public class InteresRepository : IInteresRepository
    {
        private readonly AppDbContext _db;

        public InteresRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Interes interes)
        {
            await _db.Set<Interes>().AddAsync(interes);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Set<Interes>().FindAsync(id);
            if (e == null) return;
            _db.Set<Interes>().Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Interes>> GetAllAsync()
        {
            return await _db.Set<Interes>().AsNoTracking().ToListAsync();
        }

        public async Task<List<Interes>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _db.Set<Interes>().Where(i => ids.Contains(i.Id)).ToListAsync();
        }

        public async Task<Interes> GetByIdAsync(Guid id)
        {
            return await _db.Set<Interes>().FindAsync(id);
        }

        public async Task UpdateAsync(Interes interes)
        {
            _db.Set<Interes>().Update(interes);
            await _db.SaveChangesAsync();
        }
    }
}
