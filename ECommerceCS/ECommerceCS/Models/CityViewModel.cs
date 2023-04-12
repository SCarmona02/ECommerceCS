using ECommerceCS.DAL.Entities;

namespace ECommerceCS.Models
{
    public class CityViewModel : City
    {
        public Guid StateId { get; set; }
    }
}
