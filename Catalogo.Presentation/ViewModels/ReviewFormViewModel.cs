using System.ComponentModel.DataAnnotations;

namespace CatalogoApp.Presentation.ViewModels
{
    public class ReviewFormViewModel
    {
        [Required]
        public int ItemId { get; set; }

        [Range(1, 5, ErrorMessage = "Selecciona una calificacion entre 1 y 5 estrellas.")]
        public int Calificacion { get; set; } = 5;

        [Required(ErrorMessage = "El comentario es obligatorio.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "El comentario debe tener entre 10 y 500 caracteres.")]
        public string Comentario { get; set; } = string.Empty;
    }
}
