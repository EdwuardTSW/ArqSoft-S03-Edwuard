using System.ComponentModel.DataAnnotations;

namespace CatalogoApp.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int UsuarioId { get; set; }

        public string UsuarioNombre { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "La calificacion debe estar entre 1 y 5 estrellas.")]
        public int Calificacion { get; set; }

        [Required(ErrorMessage = "El comentario es obligatorio.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "El comentario debe tener entre 10 y 500 caracteres.")]
        public string Comentario { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
