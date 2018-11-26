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

            OdbcConnection DbConnection = new OdbcConnection(Settings.CONNECTION_STRING);
            DataContext dbContext = new DataContext(Settings.ID, DbConnection);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
