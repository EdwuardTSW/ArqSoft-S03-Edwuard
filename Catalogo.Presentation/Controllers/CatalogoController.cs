using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Models;
using CatalogoApp.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CatalogoApp.Presentation.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _service;
        private readonly ReviewService _reviewService;
        public CatalogoController(ItemService service, ReviewService reviewService)
        {
            _service = service;
            _reviewService = reviewService;
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
            if (item == null)
            {
                return NotFound();
            }

            var usuarioId = ObtenerUsuarioId();
            var model = new CatalogoDetalleViewModel
            {
                Item = item,
                Reviews = _reviewService.ObtenerPorItemId(id),
                NuevaReview = new ReviewFormViewModel { ItemId = id, Calificacion = 5 },
                UsuarioAutenticado = User.Identity?.IsAuthenticated == true,
                UsuarioYaReseno = usuarioId.HasValue && _reviewService.UsuarioYaReseno(id, usuarioId.Value)
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarReview([Bind(Prefix = "NuevaReview")] ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ReviewError"] = "Revisa la calificacion y el comentario antes de enviar la reseña.";
                return RedirectToAction("Detalle", new { id = model.ItemId });
            }

            var usuarioId = ObtenerUsuarioId();
            var usuarioNombre = User.Identity?.Name;

            if (!usuarioId.HasValue || string.IsNullOrWhiteSpace(usuarioNombre))
            {
                return Challenge();
            }

            try
            {
                _reviewService.Agregar(model.ItemId, usuarioId.Value, usuarioNombre, model.Calificacion, model.Comentario);
                TempData["ReviewSuccess"] = "Tu reseña fue publicada.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ReviewError"] = ex.Message;
            }

            return RedirectToAction("Detalle", new { id = model.ItemId });
        }

        private int? ObtenerUsuarioId()
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(value, out var id) ? id : null;
        }
    }
}
