using System.ComponentModel.DataAnnotations;

namespace ECommerceCS.DAL.Entities
{
    public class Entity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Fecha de actualización")]
        public DateTime? UpdatedDate { get; set; }
    }
}
