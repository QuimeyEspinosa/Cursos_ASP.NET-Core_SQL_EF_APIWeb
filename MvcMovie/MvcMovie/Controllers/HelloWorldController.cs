using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        public IActionResult Welcome(string name, int numTimes)
        {
            ViewData["Message"] = "Hello" + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }

        #region Comentarios

        /*
        public string Welcome(string name, int ID)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");

            ///HelloWorld/Welcome/3?name=Quimey
        }
        
        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");

            ///HelloWorld/Welcome?name=Quimey&numtimes=4
        }
        */

        #endregion
    }
}
