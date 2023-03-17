﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceCS.DAL.Entities
{
    public class State : Entity
    {
        [Display(Name = "Estado")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }

        public int CitiesNumber => Cities == null ? 0 : Cities.Count;

    }
}
