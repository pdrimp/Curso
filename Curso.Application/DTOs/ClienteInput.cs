using System;
using System.ComponentModel.DataAnnotations;

namespace Curso.Application.DTOs
{
    public class ClienteInput
    {
        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        // Password optional on update
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public Guid TipoClienteId { get; set; }

        // Lista de ids de intereses
        public Guid[] InteresIds { get; set; } = Array.Empty<Guid>();
    }
}
