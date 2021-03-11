using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Concesionario.Data;
using Concesionario.Models;

namespace Concesionario.Controllers
{
    public class MarcasController : Controller
    {
        private readonly CarContext _context;

        public MarcasController(CarContext context)
        {
            _context = context;
        }

        // GET: Marcas
        public IActionResult Index(int page = 1)
        {
            var pageSize = 6; //cantidad de items por página

            var cars = _context.Car.OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            var marcas = _context.Marca.ToList();

            var totalItems = _context.Car.Count();
            IndexViewModel model = new IndexViewModel();

            #region LinQ

            var ms = cars.Join(marcas, c => c.MarcaId,
                m => m.Id,
                (c, m) => new CarViewModel
                {
                    Id = c.Id,
                    Marca = m.Nombre,
                    Descripcion = c.Descripcion,
                    Modelo = c.Modelo,
                    Precio = c.Precio,
                    Kilometros = c.Kilometros,
                    Estado = c.Estado,
                    PathImg = c.PathImg
                }).ToList();

            #endregion

            model.Cars = ms;
            model.Marcas = marcas;
            model.CurrentPage = page;
            model.TotalItems = totalItems;
            model.SizePage = pageSize;

            return View(model);
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            var cars = _context.Car.ToList();
            var marca = await _context.Marca
                .FirstOrDefaultAsync(m => m.Id == id);

            if (marca == null)
            {
                return NotFound();
            }

            var mcars = (from c in cars
                         where c.MarcaId == marca.Id
                         select new CarViewModel
                         {
                             Id = c.Id,
                             Marca = marca.Nombre,
                             Descripcion = c.Descripcion,
                             Modelo = c.Modelo,
                             Precio = c.Precio,
                             Kilometros = c.Kilometros,
                             Estado = c.Estado,
                             PathImg = c.PathImg
                         }).ToList();

            return View(mcars);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marca.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Marca marca)
        {
            if (id != marca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(marca.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marca = await _context.Marca.FindAsync(id);

            var mcars = (from c in _context.Car
                         where c.MarcaId == marca.Id
                         select c).ToList();

            _context.Car.RemoveRange(mcars);
            _context.Marca.Remove(marca);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
            return _context.Marca.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var marcas = await _context.Marca.ToListAsync();
            List<Marca> marcaSearch = new List<Marca>();
            string auxSearch;

            if (!String.IsNullOrEmpty(searchString))
            {
                auxSearch = searchString.ToLower();

                marcaSearch = (from marca in marcas
                               where (marca.Nombre.ToString().ToLower() + " " + marca.Nombre.ToString().ToLower()).Contains(auxSearch)
                               select marca).ToList();
            }

            return View(marcaSearch);
        }
    }
}
