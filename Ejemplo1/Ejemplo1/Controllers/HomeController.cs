using Ejemplo1.Models;
using Ejemplo1.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Controllers
{
    public class HomeController : Controller
    {
        private IProductoComercio productoComercio;
        private IHostingEnvironment hosting;

        public HomeController(IProductoComercio prodComercio, IHostingEnvironment hostingEnvironment)
        {
            productoComercio = prodComercio;
            hosting = hostingEnvironment;
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public ViewResult Index(int id)
        {
            List<Producto> productos = productoComercio.GetProductos();

            return View(productos);
        }

        [Route("Home/Details/{id?}")]
        public ViewResult Details(int? id)
        {
            DetailsView details = new DetailsView();

            //Si es nulo forzamos a que busque los detalles del producto 1
            details.Producto = productoComercio.GetDatosProducto(id ?? 1);
            details.Titulo = "Detalles producto";

            return View(details);
        }

        [Route("Home/Create")]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [Route("Home/Create")]
        [HttpPost]
        public IActionResult Create(CreateProductoModel p)
        {
            if (ModelState.IsValid)
            {
                string guidImagen = null;

                if (p.Foto != null)
                {
                    string ficherosImagenes = Path.Combine(hosting.WebRootPath, "images");
                    guidImagen = Guid.NewGuid().ToString() + p.Foto.FileName;
                    string rutaDefinitiva = Path.Combine(ficherosImagenes, guidImagen);
                    using (var fileStream = new FileStream(rutaDefinitiva, FileMode.Create))
                    {
                        p.Foto.CopyTo(fileStream);
                    }
                }
                Producto newProd = new Producto();
                newProd.Descripcion = p.Descripcion;
                newProd.Precio = p.Precio;
                newProd.RutaFoto = guidImagen; //Se guarda el identificador de la imagen, no la ruta completa

                productoComercio.AddProducto(newProd);
                return RedirectToAction("details", new { id = newProd.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Producto prod = productoComercio.GetDatosProducto(id);
            EditProductoModel prodEdit = new EditProductoModel
            {
                Id = prod.Id,
                Descripcion = prod.Descripcion,
                Precio = prod.Precio,
                RutaFotoExistente = prod.RutaFoto
            };

            return View(prodEdit);
        }

        public IActionResult Edit(EditProductoModel editProducto)
        {
            if (ModelState.IsValid)
            {
                Producto prod = productoComercio.GetDatosProducto(editProducto.Id);

                prod.Descripcion = editProducto.Descripcion;
                prod.Precio = editProducto.Precio;

                if (editProducto.Foto != null)
                {
                    //Al subir una foto debe borrarse la anterior
                    if (editProducto.RutaFotoExistente != null)
                    {
                        string ruta = Path.Combine(hosting.WebRootPath, "images", editProducto.RutaFotoExistente);
                        System.IO.File.Delete(ruta);
                    }

                    //Guardamos la nueva foto en wwwroot/images
                    prod.RutaFoto = SubirImagen(editProducto);
                }

                Producto prodEditado = productoComercio.EditProducto(prod);

                return RedirectToAction("index");
            }

            return View(editProducto);
        }

        private string SubirImagen(EditProductoModel prod)
        {
            string nombreArchivo = null;

            if (prod.Foto != null)
            {
                string ficherosImagenes = Path.Combine(hosting.WebRootPath, "images");
                nombreArchivo = Guid.NewGuid().ToString() + prod.Foto.FileName;
                string ruta = Path.Combine(ficherosImagenes, nombreArchivo);

                using (var fileStream = new FileStream(ruta, FileMode.Create))
                {
                    prod.Foto.CopyTo(fileStream);
                }
            }

            return nombreArchivo;
        }

        public IActionResult Delete(Producto deleteProd)
        {
            if (ModelState.IsValid)
            {
                productoComercio.DeleteProducto(deleteProd.Id);
            }

            return RedirectToAction("index");
        }



        #region Comentarios

        /*
        [HttpPost]
        public IActionResult Create(Producto p)
        {
            if (ModelState.IsValid)
            {
                Producto newProd = productoComercio.AddProducto(p);
                return RedirectToAction("details", new { id = newProd.Id });
            }

            return View();
        }
        */


        /*
        public ViewResult Details()
        {
            Producto prod = productoComercio.GetDatosProducto(2);

            ViewData["Cabecera"] = "Lista de Productos";
            ViewData["Producto"] = prod;

            return View(prod);
        }
        */

        /*
        public JsonResult Index()
        {
            return Json(new { id = 2, nombre = "Quimey" });
        }
        */

        //public JsonResult Details()
        //{
        //    Producto prod = productoComercio.GetDatosProducto(1);
        //
        //    return Json(prod);
        //}

        #endregion
    }
}
