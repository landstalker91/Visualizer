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

            int id = 3864903;

            OdbcConnection DbConnection = new OdbcConnection("DSN=GAAMDB_64;Server=10.1.8.95;UID=itam;PWD=Qaz12345;Database=GAAMDB;");
            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();
            DbCommand.CommandText = "SELECT * FROM amPortfolio";
            OdbcDataReader DbReader = DbCommand.ExecuteReader();


            DbContext dbContext = new DbContext(id);
            int fCount = DbReader.FieldCount;
            Console.Write(":");
        
            //

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
