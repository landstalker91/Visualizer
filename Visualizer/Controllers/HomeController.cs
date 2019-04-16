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
        OdbcConnection DbConnection = new OdbcConnection(Settings.CONNECTION_STRING);
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Graph(int id)
        {
            if (id != 0) {
            try
                {
                    ElementsNetwork network = new ElementsNetwork(id, DbConnection);
                    ViewBag.network = network;
                }
            catch (Exception ex)
                {
                    ViewBag.Error = ex.ToString();
                    return View("Error");
                }

                return View();
            } else {
                return View("Index");
            }
        }
        [HttpPost]
        public JsonResult ajaxGetNode(int id)
        {
            try
            {
                Node node = new Node(id, DbConnection);
                return Json(new {
                    returnCode = "Success",
                    message = "Успешно",
                    id = node.Id,
                    name = node.Name,
                    category = node.Category,
                    subCategory = node.SubCategory,
                    status = node.Status,
                    modelShortName = node.ModelShortName,
                    modelLongName = node.ModelLongName,
                    cost = node.Cost,
                    location = node.Location
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnCode = "Server error: " + ex.ToString(), message = "Что-то пошло не так :(\nОбратитесь к администратору" });
            }
        }
        [HttpPost]
        public JsonResult ajaxGetLink(int id)
        {
            try
            {
                Link link = new Link(id, DbConnection);
                return Json(new
                {
                    returnCode = "Success",
                    message = "Успешно",
                    id = link.Id,
                    weight = link.Weight,
                    type = link.Type
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnCode = "Server error: " + ex.ToString(), message = "Что-то пошло не так :(\nОбратитесь к администратору" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
