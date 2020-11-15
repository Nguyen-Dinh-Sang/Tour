using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourMVC.Models;

namespace TourMVC.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly TourDBContext context;

        public IActionResult Index()
        {
            return View();
        }
    }
}