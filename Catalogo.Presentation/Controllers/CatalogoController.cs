using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
namespace CatalogoApp.Presentation.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _service;
        public CatalogoController(ItemService service)
        {
            _service = service;
        }
        public IActionResult Index(string? marca, string? categoria, string? busqueda, string? orden)
        {
            var items = _service.Filtrar(marca, categoria, busqueda, orden);
            ViewBag.Marcas = _service.ObtenerMarcas();
            ViewBag.Categorias = _service.ObtenerCategorias();
            ViewBag.MarcaActual = marca;
            ViewBag.CategoriaActual = categoria;
            ViewBag.BusquedaActual = busqueda;
            ViewBag.OrdenActual = orden;
            ViewBag.TotalResultados = items.Count;
            return View(items);
        }
        public IActionResult Detalle(int id)
        {
            var item = _service.ObtenerPorId(id);
            return item == null ? NotFound() : View(item);
        }
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            _service.Agregar(item);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}