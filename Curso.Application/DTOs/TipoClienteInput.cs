using System.ComponentModel.DataAnnotations;
using System;

namespace Curso.Application.DTOs
{
    public class TipoClienteInput
    {
        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string Descripcion { get; set; }
    }
}
