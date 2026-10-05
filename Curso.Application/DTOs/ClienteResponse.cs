using System;
using System.Collections.Generic;

namespace Curso.Application.DTOs
{
    public class ClienteResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public Guid TipoClienteId { get; set; }
        public string TipoClienteDescripcion { get; set; }
        public List<Guid> InteresIds { get; set; } = new List<Guid>();
    }
}
