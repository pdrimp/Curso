using Microsoft.EntityFrameworkCore;
using Curso.Domain.Entities;

namespace Curso.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TipoCliente> TiposClientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoCliente>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Descripcion).IsRequired().HasMaxLength(80);
            });
        }
    }
}
