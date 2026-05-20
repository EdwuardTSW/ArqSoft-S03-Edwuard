using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;
namespace CatalogoApp.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _repo;
        public ItemService(IItemRepository repo)
        {
            _repo = repo;
        }
        public List<Item> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }
        public Item? ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }
        public void Agregar(Item item)
        {
            _repo.Agregar(item);
        }
        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }
        public List<Item> Filtrar(string? marca, string? categoria, string? busqueda, string? orden)
        {
            var items = _repo.ObtenerTodos().AsEnumerable();
            if (!string.IsNullOrWhiteSpace(marca))
            {
                items = items.Where(i => i.Marca == marca);
            }
            if (!string.IsNullOrWhiteSpace(categoria))
            {
                items = items.Where(i => i.Categoria == categoria);
            }
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                items = items.Where(i =>
                    i.Marca.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                    i.Modelo.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                    i.Categoria.Contains(busqueda, StringComparison.OrdinalIgnoreCase));
            }
            items = orden switch
            {
                "precio-asc" => items.OrderBy(i => i.Precio),
                "precio-desc" => items.OrderByDescending(i => i.Precio),
                "ano-desc" => items.OrderByDescending(i => i.Ano),
                "ano-asc" => items.OrderBy(i => i.Ano),
                _ => items.OrderBy(i => i.Marca).ThenBy(i => i.Modelo)
            };
            return items.ToList();
        }
        public List<string> ObtenerMarcas()
        {
            return _repo.ObtenerTodos()
                .Select(i => i.Marca)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(m => m)
                .ToList();
        }
        public List<string> ObtenerCategorias()
        {
            return _repo.ObtenerTodos()
                .Select(i => i.Categoria)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }
    }
}