using System.ComponentModel.DataAnnotations;

namespace ECommerceCS.DAL.Entities
{
    public class Country : Entity
    {
        [Display(Name = "País")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Dpto/Estados")]
        public ICollection<State> States { get; set; }

        [Display(Name = "Número Estados")]
        public int StatesNumber => States == null ? 0 : States.Count;
    }
}
