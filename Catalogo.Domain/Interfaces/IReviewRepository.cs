using CatalogoApp.Domain.Models;

namespace CatalogoApp.Domain.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> ObtenerTodos();
        List<Review> ObtenerPorItemId(int itemId);
        Review? ObtenerPorItemYUsuario(int itemId, int usuarioId);
        void Agregar(Review review);
    }
}
