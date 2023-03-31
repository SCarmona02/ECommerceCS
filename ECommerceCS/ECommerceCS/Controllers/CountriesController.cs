using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceCS.DAL;
using ECommerceCS.DAL.Entities;
using ECommerceCS.Models;

namespace ECommerceCS.Controllers
{
    public class CountriesController : Controller
    {
        #region Costructor

        private readonly DatabaseContext _context;

        public CountriesController(DatabaseContext context)
        {
            _context = context;
        }

        #endregion

        #region Private Methods
        private async Task<Country> GetCountryById(Guid? countryId)
        {
            Country country = await _context.Countries
                .Include(country => country.States)
                .FirstOrDefaultAsync(country => country.Id == countryId);
            return country;
        }

        private async Task<State> GetStateById(Guid? stateId)
        {
            State state = await _context.States
                .Include(state => state.Country)
                .Include(state => state.Cities)
                .FirstOrDefaultAsync(state => state.Id == stateId);
            return state;
        }

        #endregion

        #region Country Actions

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.Include(country => country.States).ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(Guid? countryId)
        {
            if (countryId == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(country => country.States)
                .FirstOrDefaultAsync(m => m.Id == countryId);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    country.CreatedDate = DateTime.Now;
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);
        }
        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(Guid? countryId)
        {
            if (countryId == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await GetCountryById(countryId);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid countryId, Country country)
        {
            if (countryId != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    country.UpdatedDate = DateTime.Now;
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(Guid? countryId)
        {
            if (countryId == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(country => country.States)
                .FirstOrDefaultAsync(country => country.Id == countryId);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? countryId)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'DatabaseContext.Countries'  is null.");
            }
            var country = await GetCountryById(countryId);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region State Actions
        public async Task<IActionResult> AddState(Guid? countryId)
        {
            if (countryId == null)
            {
                return NotFound();
            }

            Country country = await GetCountryById(countryId);
            if (country == null)
            {
                return NotFound();
            }

            StateViewModel stateViewModel = new()
            {
                CountryId = country.Id
            };

            return View(stateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddState(StateViewModel stateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    State state = new State()
                    {
                        Cities = new List<City>(),
                        Country = await GetCountryById(stateViewModel.CountryId),
                        Name = stateViewModel.Name,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null,
                    };

                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { stateViewModel.CountryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Departamento/Estado con el mismo nombre en este país.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(stateViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditState(Guid? stateId)
        {
            if (stateId == null || _context.States == null)
            {
                return NotFound();
            }

            State state = await _context.States
                .Include(state => state.Country)
                .FirstOrDefaultAsync(state => state.Id == stateId);

            if (state == null)
            {
                return NotFound();
            }
            StateViewModel stateViewModel = new()
            {
                CountryId = state.Country.Id,
                Id = state.Id,
                Name = state.Name,
                CreatedDate = DateTime.Now,
            };

            return View(stateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(Guid stateId, StateViewModel stateViewModel)
        {
            if (stateId != stateViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        Id = stateViewModel.Id,
                        Name = stateViewModel.Name,
                        CreatedDate = stateViewModel.CreatedDate,
                        UpdatedDate = DateTime.Now,
                    };

                    _context.Update(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { stateViewModel.CountryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un estado con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(stateViewModel);
        }

        public async Task<IActionResult> DetailsState(Guid? stateId)
        {
            if (stateId == null || _context.Countries == null)
            {
                return NotFound();
            }

            State state = await GetStateById(stateId);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        public async Task<IActionResult> DeleteState(Guid? stateId)
        {
            if (stateId == null || _context.States == null)
            {
                return NotFound();
            }

            State state = await GetStateById(stateId);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        [HttpPost, ActionName("DeleteState")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStateConfirmed(Guid? stateId)
        {
            if (_context.States == null)
            {
                return Problem("Entity set 'DatabaseContext.States'  is null.");
            }
            State state = await GetStateById(stateId);
            if (state != null)
            {
                _context.States.Remove(state);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { countryId = state.Country.Id });
        }



        #endregion

    }
}
