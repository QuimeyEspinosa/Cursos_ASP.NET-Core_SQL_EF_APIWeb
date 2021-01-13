using Ejemplo1.Models;
using Ejemplo1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1.Controllers
{
    public class HomeController : Controller
    {
        private IProductoComercio productoComercio;

        public HomeController(IProductoComercio prodComercio)
        {
            productoComercio = prodComercio;
        }

        public ViewResult Index()
        {
            List<Producto> productos = productoComercio.GetProductos();

            return View(productos);
        }

        public ViewResult Details()
        {
            DetailsView details = new DetailsView();

            details.Producto = productoComercio.GetDatosProducto(2);
            details.Titulo = "Lista Productos viewModel";

            return View(details);
        }




        #region Comentarios

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
