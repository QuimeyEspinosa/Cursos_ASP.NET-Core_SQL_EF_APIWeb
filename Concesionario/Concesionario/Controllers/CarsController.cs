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
using Concesionario.Helpers;
using Microsoft.AspNetCore.Http;


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

        #region Index Sin paginado        

        /*
        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Car.ToListAsync());
        }
        */

        #endregion

        #region Index Paginado 1er intento
        /*
        [HttpGet]
        public async Task<ActionResult<List<Car>>> Index([FromQuery]Pagination p)
        {
            var queryable = _context.Car.AsQueryable();

            await HttpContext.InsertAnswerPaginationParams(queryable, p.ItemsToShow);

            return View(await queryable.Paginar(p).ToListAsync());
        }
        */
        #endregion

        #region Index llamadas a la base

        /*
        public IActionResult Index(int page = 1)
        {
            var pageSize = 6; //cantidad de items por página

            var cars = _context.Car.OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            var marcas = _context.Marca.ToList();
            var totalItems = _context.Car.Count();

            IndexViewModel model = new IndexViewModel();            

            foreach (Car item in cars)
            {
                CarViewModel newCar = new CarViewModel();

                var marcaVehiculo = marcas.Where(x => x.Id == item.MarcaId)
                    .Select(x => new { x.Nombre }).ToList();

                newCar.Id = item.Id;
                newCar.Descripcion = item.Descripcion;
                newCar.Modelo = item.Modelo;
                newCar.Precio = item.Precio;
                newCar.Estado = item.Estado;
                newCar.PathImg = item.PathImg;
                newCar.MarcaId = item.MarcaId;
                newCar.Marca = marcaVehiculo[0].Nombre;

                model.Cars.Add(newCar);
            }

            model.CurrentPage = page;
            model.TotalItems = totalItems;
            model.SizePage = pageSize;

            return View(model);
        }
        */

        #endregion

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 6; //cantidad de items por página

            var marcas = await _context.Marca.ToListAsync();
            var cars = await _context.Car.OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalItems = _context.Car.Count();
            IndexViewModel model = new IndexViewModel();

            #region LinQ

            /*
            var qs = (from car in cars
                      join marca in marcas
                      on car.MarcaId equals marca.Id
                      select new CarViewModel
                      {
                          Id = car.Id,
                          Marca = marca.Nombre,
                          Descripcion = car.Descripcion,
                          Modelo = car.Modelo,
                          Precio = car.Precio,
                          Kilometros = car.Kilometros,
                          Estado = car.Estado,
                          PathImg = car.PathImg

                      }).ToList();
            */

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
            model.CurrentPage = page;
            model.TotalItems = totalItems;
            model.SizePage = pageSize;

            return View(model);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*
            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            */

            var cars = await _context.Car.ToListAsync();
            var marcas = await _context.Marca.ToListAsync();

            var car = (from c in cars
                       where c.Id == id
                       join marca in marcas
                       on c.MarcaId equals marca.Id
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

            if (car == null)
            {
                return NotFound();
            }

            return View(car[0]);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            CreateCarViewModel model = new CreateCarViewModel();

            model.Marcas = _context.Marca.Select(marca => new SelectListItem() { Value = marca.Id.ToString(), Text = marca.Nombre.ToString() })
                .ToList();

            return View(model);
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Marca,Descripcion,Modelo,Precio,Kilometros,Estado,Foto")] CreateCarViewModel car)
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
                newCar.Kilometros = car.Kilometros;
                newCar.MarcaId = car.Marca;
                newCar.PathImg = guidImagen;

                _context.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
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
                Kilometros = car.Kilometros,
                ExistPathImg = car.PathImg,
                Marca = car.MarcaId
            };

            editCar.Marcas = _context.Marca.Select(marca => new SelectListItem() { Value = marca.Id.ToString(), Text = marca.Nombre.ToString() })
                .ToList();

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marca,Descripcion,Modelo,Precio,Estado,Foto")] EditCarViewModel car)
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
                    myCar.Kilometros = car.Kilometros;
                    myCar.MarcaId = car.Marca;

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
            string auxSearch;

            if (!String.IsNullOrEmpty(searchString))
            {
                auxSearch = searchString.ToLower();
                auxCars = myCars.Where(c => c.Descripcion.ToLower().Contains(auxSearch)).ToList<Car>();
            }

            return View(auxCars);
        }

    }
}
