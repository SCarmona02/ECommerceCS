using ECommerceCS.DAL;
using ECommerceCS.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCS.Services
{
    public class DropDownListsHelper : IDropDownListsHelper
    {
        private readonly DatabaseContext _context;

        public async Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync()
        {
            List<SelectListItem> listCategories = await _context.Categories
                .Select(category => new SelectListItem
                {
                    Text = category.Name, //Col
                    Value = category.Id.ToString(), //Guid                    
                })
                .OrderBy(category => category.Text)
                .ToListAsync();

            listCategories.Insert(0, new SelectListItem
            {
                Text = "Selecione una categoría...",
                Value = "0",
            });

            return listCategories;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCountriesAsync()
        {
            List<SelectListItem> listCountries = await _context.Countries
                .Select(country => new SelectListItem
                {
                    Text = country.Name, //Col
                    Value = country.Id.ToString(), //Guid                    
                })
                .OrderBy(country => country
                .Text)
                .ToListAsync();

            listCountries.Insert(0, new SelectListItem
            {
                Text = "Selecione un país...",
                Value = "0",
            });

            return listCountries;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLStatesAsync(Guid countryId)
        {
            List<SelectListItem> listStatesByCountryId = await _context.States
                .Where(state => state.Country.Id == countryId)
                .Select(state => new SelectListItem
                {
                    Text = state.Name,
                    Value = state.Id.ToString(),
                })
                .OrderBy(state => state.Text)
                .ToListAsync();

            listStatesByCountryId.Insert(0, new SelectListItem
            {
                Text = "Selecione un estado...",
                Value = "0",
            });

            return listStatesByCountryId;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCitiesAsync(Guid stateId)
        {
            List<SelectListItem> listCitiesByStateId = await _context.Cities
                .Where(city => city.State.Id == stateId)
                .Select(city => new SelectListItem
                {
                    Text = city.Name,
                    Value = city.Id.ToString(),
                })
                .OrderBy(city => city.Text)
                .ToListAsync();

            listCitiesByStateId.Insert(0, new SelectListItem
            {
                Text = "Selecione una ciudad...",
                Value = "0",
            });

            return listCitiesByStateId;
        }

    }
}
