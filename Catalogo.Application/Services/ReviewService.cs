using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IItemRepository _itemRepository;

        public ReviewService(IReviewRepository reviewRepository, IItemRepository itemRepository)
        {
            _reviewRepository = reviewRepository;
            _itemRepository = itemRepository;
        }

        public List<Review> ObtenerPorItemId(int itemId)
        {
            return _reviewRepository.ObtenerPorItemId(itemId);
        }

        public bool UsuarioYaReseno(int itemId, int usuarioId)
        {
            return _reviewRepository.ObtenerPorItemYUsuario(itemId, usuarioId) != null;
        }

        public void Agregar(int itemId, int usuarioId, string usuarioNombre, int calificacion, string comentario)
        {
            if (_itemRepository.ObtenerPorId(itemId) == null)
            {
                throw new InvalidOperationException("La moto no existe.");
            }

            if (UsuarioYaReseno(itemId, usuarioId))
            {
                throw new InvalidOperationException("Ya reseñaste esta moto.");
            }

            var review = new Review
            {
                ItemId = itemId,
                UsuarioId = usuarioId,
                UsuarioNombre = usuarioNombre,
                Calificacion = calificacion,
                Comentario = comentario.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _reviewRepository.Agregar(review);
        }
    }
}
