using System;

namespace Curso.Domain.Entities
{
    public class Interes
    {
        public Guid Id { get; private set; }

        public string Descripcion { get; private set; }

        protected Interes() { }

        // Navigation property for many-to-many with Cliente
        public ICollection<Curso.Domain.Entities.Cliente> Clientes { get; private set; } = new List<Curso.Domain.Entities.Cliente>();

        public Interes(string descripcion)
        {
            SetDescripcion(descripcion);
            Id = Guid.NewGuid();
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("Descripcion es requerida", nameof(descripcion));
            if (descripcion.Length < 2 || descripcion.Length > 80)
                throw new ArgumentException("Descripcion debe tener entre 2 y 80 caracteres", nameof(descripcion));
            Descripcion = descripcion.Trim();
        }
    }
}
