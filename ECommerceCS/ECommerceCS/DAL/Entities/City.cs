using System.ComponentModel.DataAnnotations;

namespace ECommerceCS.DAL.Entities
{
    public class City : Entity
    {
        [Display(Name = "Ciudad")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public State State { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
