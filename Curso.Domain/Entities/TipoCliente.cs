using System;

namespace Curso.Domain.Entities
{
    public class TipoCliente
    {
        public Guid Id { get; private set; }

        public string Descripcion { get; private set; }

        // Parameterless constructor for EF
        protected TipoCliente() { }

        public TipoCliente(string descripcion)
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
