using System.ComponentModel.DataAnnotations;

namespace Curso.Application.DTOs
{
    public class InteresInput
    {
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "La descripción debe tener entre 2 y 80 caracteres.")]
        public string Descripcion { get; set; }
    }
}
