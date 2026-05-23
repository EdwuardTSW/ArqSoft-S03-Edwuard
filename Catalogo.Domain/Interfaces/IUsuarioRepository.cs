using CatalogoApp.Domain.Models;

namespace CatalogoApp.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos();
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorEmail(string email);
        bool ExisteEmail(string email);
        void Agregar(Usuario usuario);
    }
}
