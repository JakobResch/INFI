using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1Website_Vogt.Models {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}