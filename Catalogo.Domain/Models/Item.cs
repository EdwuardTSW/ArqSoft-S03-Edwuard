using System.ComponentModel.DataAnnotations;

namespace CatalogoApp.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [StringLength(60, ErrorMessage = "La marca no puede superar los 60 caracteres.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [StringLength(80, ErrorMessage = "El modelo no puede superar los 80 caracteres.")]
        public string Modelo { get; set; } = string.Empty;

        [Display(Name = "Año")]
        [Range(2000, 2030, ErrorMessage = "El año debe estar entre 2000 y 2030.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "La cilindrada es obligatoria.")]
        [StringLength(30, ErrorMessage = "La cilindrada no puede superar los 30 caracteres.")]
        public string Cilindrada { get; set; } = string.Empty;

        [Range(1, 9999999, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(40, ErrorMessage = "La categoría no puede superar los 40 caracteres.")]
        public string Categoria { get; set; } = string.Empty;

        [Display(Name = "URL de imagen")]
        [Required(ErrorMessage = "La URL de imagen es obligatoria.")]
        [Url(ErrorMessage = "Ingresa una URL válida para la imagen.")]
        public string ImagenUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "La potencia es obligatoria.")]
        [StringLength(30, ErrorMessage = "La potencia no puede superar los 30 caracteres.")]
        public string Potencia { get; set; } = string.Empty;

        [Display(Name = "Transmisión")]
        [Required(ErrorMessage = "La transmisión es obligatoria.")]
        [StringLength(50, ErrorMessage = "La transmisión no puede superar los 50 caracteres.")]
        public string Transmision { get; set; } = string.Empty;

        [Required(ErrorMessage = "El color es obligatorio.")]
        [StringLength(60, ErrorMessage = "El color no puede superar los 60 caracteres.")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "La descripción debe tener entre 20 y 500 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;
    }
}