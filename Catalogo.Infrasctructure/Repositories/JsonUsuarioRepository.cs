using System.Text.Json;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonUsuarioRepository : IUsuarioRepository
    {
        private readonly string _filePath;
        private readonly object _lock = new();

        public JsonUsuarioRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Usuario> ObtenerTodos()
        {
            lock (_lock)
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Usuario>();
                }

                var json = File.ReadAllText(_filePath);

                return JsonSerializer.Deserialize<List<Usuario>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Usuario>();
            }
        }

        public Usuario? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(u => u.Id == id);
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            return ObtenerTodos().FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExisteEmail(string email)
        {
            return ObtenerPorEmail(email) != null;
        }

        public void Agregar(Usuario usuario)
        {
            lock (_lock)
            {
                var usuarios = LeerSinBloqueo();
                usuario.Id = usuarios.Any() ? usuarios.Max(u => u.Id) + 1 : 1;
                usuarios.Add(usuario);
                GuardarSinBloqueo(usuarios);
            }
        }

        private List<Usuario> LeerSinBloqueo()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Usuario>();
            }

            var json = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<Usuario>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Usuario>();
        }

        private void GuardarSinBloqueo(List<Usuario> usuarios)
        {
            var directory = Path.GetDirectoryName(_filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
    }
}
