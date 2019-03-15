using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Visualizer.Models;
using System.Data.Odbc;

namespace Visualizer.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Graph(int id)
        {
            if (id != 0) {
                OdbcConnection DbConnection = new OdbcConnection(Settings.CONNECTION_STRING);
                ElementsNetwork network = new ElementsNetwork(id, DbConnection);
                ViewBag.network = network;
                return View();
            } else {
                return View("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
