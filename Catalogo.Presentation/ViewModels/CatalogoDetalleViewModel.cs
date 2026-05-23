using CatalogoApp.Domain.Models;

namespace CatalogoApp.Presentation.ViewModels
{
    public class CatalogoDetalleViewModel
    {
        public Item Item { get; set; } = new();

        public List<Review> Reviews { get; set; } = new();

        public ReviewFormViewModel NuevaReview { get; set; } = new();

        public bool UsuarioAutenticado { get; set; }

        public bool UsuarioYaReseno { get; set; }

        public double PromedioCalificacion => Reviews.Count == 0 ? 0 : Reviews.Average(r => r.Calificacion);
    }
}
