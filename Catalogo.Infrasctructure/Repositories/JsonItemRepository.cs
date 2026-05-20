using System.Text.Json;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;
namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonItemRepository : IItemRepository
    {
        private readonly string _filePath;
        public JsonItemRepository(string filePath)
        {
            _filePath = filePath;
        }
        public List<Item> ObtenerTodos()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Item>();
            }
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Item>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Item>();
        }
        public Item? ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(i => i.Id == id);
        }
        public void Agregar(Item item)
        {
            var items = ObtenerTodos();
            item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            items.Add(item);
            Guardar(items);
        }
        public void Eliminar(int id)
        {
            var items = ObtenerTodos();
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return;
            }
            items.Remove(item);
            Guardar(items);
        }
        private void Guardar(List<Item> items)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_filePath, json);
        }
    }
}