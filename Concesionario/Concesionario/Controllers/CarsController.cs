using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Concesionario.Data;
using Concesionario.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Concesionario.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarContext _context;
        private IWebHostEnvironment hosting;

        public CarsController(CarContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            hosting = hostingEnvironment;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Car.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descripcion,Modelo,Precio,Estado,Foto")] CreateCarViewModel car)
        {
            if (ModelState.IsValid)
            {
                string guidImagen = null;

                if (car.Foto != null)
                {
                    string ficherosImagenes = Path.Combine(hosting.WebRootPath, "img");
                    guidImagen = Guid.NewGuid().ToString() + car.Foto.FileName;
                    string rutaDefinitiva = Path.Combine(ficherosImagenes, guidImagen);
                    using (var fileStream = new FileStream(rutaDefinitiva, FileMode.Create))
                    {
                        car.Foto.CopyTo(fileStream);
                    }
                }

                Car newCar = new Car();
                newCar.Descripcion = car.Descripcion;
                newCar.Modelo = car.Modelo;
                newCar.Precio = car.Precio;
                newCar.Estado = car.Estado;
                newCar.PathImg = guidImagen;

                _context.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }


        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);

            EditCarViewModel editCar = new EditCarViewModel
            {
                Id = car.Id,
                Descripcion = car.Descripcion,
                Modelo = car.Modelo,
                Precio = car.Precio,
                Estado = car.Estado,
                ExistPathImg = car.PathImg
            };

            if (car == null)
            {
                return NotFound();
            }

            return View(editCar);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Modelo,Precio,Estado,Foto")] EditCarViewModel car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Car myCar = _context.Car.Find(id);

                    myCar.Descripcion = car.Descripcion;
                    myCar.Modelo = car.Modelo;
                    myCar.Precio = car.Precio;
                    myCar.Estado = car.Estado;

                    if (car.Foto != null)
                    {
                        if (car.ExistPathImg != null)
                        {
                            string pathImg = Path.Combine(hosting.WebRootPath, "img", car.ExistPathImg);
                            System.IO.File.Delete(pathImg);
                        }

                        myCar.PathImg = UploadImage(car);
                    }

                    _context.Update(myCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }

        private string UploadImage(EditCarViewModel carEdit)
        {
            string nombreArchivo = null;

            if (carEdit.Foto != null)
            {
                string ficherosImagenes = Path.Combine(hosting.WebRootPath, "img");
                nombreArchivo = Guid.NewGuid().ToString() + carEdit.Foto.FileName;
                string ruta = Path.Combine(ficherosImagenes, nombreArchivo);

                using (var fileStream = new FileStream(ruta, FileMode.Create))
                {
                    carEdit.Foto.CopyTo(fileStream);
                }
            }

            return nombreArchivo;
        }

        public async Task<IActionResult> Search(string searchString)
        {
            List<Car> myCars = await _context.Car.ToListAsync();
            List<Car> auxCars = new List<Car>();

            if (!String.IsNullOrEmpty(searchString))
            {
                auxCars = myCars.Where(c => c.Descripcion.Contains(searchString)).ToList<Car>();
            }

            return View(auxCars);
        }

    }
}
