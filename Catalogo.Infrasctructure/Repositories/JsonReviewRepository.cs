using System.Text.Json;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonReviewRepository : IReviewRepository
    {
        private readonly string _filePath;
        private readonly object _lock = new();

        public JsonReviewRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Review> ObtenerTodos()
        {
            lock (_lock)
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Review>();
                }

                var json = File.ReadAllText(_filePath);

                return JsonSerializer.Deserialize<List<Review>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Review>();
            }
        }

        public List<Review> ObtenerPorItemId(int itemId)
        {
            return ObtenerTodos()
                .Where(r => r.ItemId == itemId)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public Review? ObtenerPorItemYUsuario(int itemId, int usuarioId)
        {
            return ObtenerTodos().FirstOrDefault(r => r.ItemId == itemId && r.UsuarioId == usuarioId);
        }

        public void Agregar(Review review)
        {
            lock (_lock)
            {
                var reviews = LeerSinBloqueo();
                review.Id = reviews.Any() ? reviews.Max(r => r.Id) + 1 : 1;
                reviews.Add(review);
                GuardarSinBloqueo(reviews);
            }
        }

        private List<Review> LeerSinBloqueo()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Review>();
            }

            var json = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<Review>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Review>();
        }

        private void GuardarSinBloqueo(List<Review> reviews)
        {
            var directory = Path.GetDirectoryName(_filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(reviews, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
    }
}
