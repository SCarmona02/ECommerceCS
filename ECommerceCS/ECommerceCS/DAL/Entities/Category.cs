using System.ComponentModel.DataAnnotations;

namespace ECommerceCS.DAL.Entities
{
    public class Category : Entity
    {
        [Display(Name = "Categoría")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe ser de {1} caracteres")]
        public string? Description { get; set; }
    }
}
