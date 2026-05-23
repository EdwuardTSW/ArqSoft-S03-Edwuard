using System.Security.Cryptography;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class UsuarioService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;

        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        public Usuario Registrar(string nombre, string email, string password)
        {
            email = email.Trim().ToLowerInvariant();

            if (_repo.ExisteEmail(email))
            {
                throw new InvalidOperationException("Ya existe una cuenta registrada con ese email.");
            }

            var usuario = new Usuario
            {
                Nombre = nombre.Trim(),
                Email = email,
                PasswordHash = HashPassword(password),
                Rol = "Usuario",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Agregar(usuario);

            return usuario;
        }

        public Usuario? ValidarCredenciales(string email, string password)
        {
            var usuario = _repo.ObtenerPorEmail(email.Trim().ToLowerInvariant());

            if (usuario == null || !VerifyPassword(password, usuario.PasswordHash))
            {
                return null;
            }

            return usuario;
        }

        public void CrearAdminInicialSiNoExiste(string nombre, string email, string password)
        {
            email = email.Trim().ToLowerInvariant();

            if (_repo.ExisteAdmin() || _repo.ExisteEmail(email))
            {
                return;
            }

            var usuario = new Usuario
            {
                Nombre = nombre.Trim(),
                Email = email,
                PasswordHash = HashPassword(password),
                Rol = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Agregar(usuario);
        }

        private static string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        private static bool VerifyPassword(string password, string passwordHash)
        {
            var parts = passwordHash.Split('.');

            if (parts.Length != 3 || !int.TryParse(parts[0], out var iterations))
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[1]);
            var expectedHash = Convert.FromBase64String(parts[2]);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
