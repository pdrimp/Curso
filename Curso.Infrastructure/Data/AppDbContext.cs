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
        public DbSet<Curso.Domain.Entities.Interes> Intereses { get; set; }
        public DbSet<Curso.Domain.Entities.Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoCliente>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Descripcion).IsRequired().HasMaxLength(80);
            });

            modelBuilder.Entity<Curso.Domain.Entities.Interes>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Descripcion).IsRequired().HasMaxLength(80);
            });

            modelBuilder.Entity<Curso.Domain.Entities.Cliente>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Nombre).IsRequired().HasMaxLength(80);
                b.Property(e => e.Correo).IsRequired().HasMaxLength(320);
                b.HasIndex(e => e.Correo).IsUnique();

                b.HasOne(e => e.TipoCliente).WithMany().HasForeignKey(e => e.TipoClienteId).OnDelete(DeleteBehavior.Restrict);

                b.HasMany(e => e.Intereses).WithMany("Clientes");
            });
        }
    }
}
