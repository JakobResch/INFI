using _1Website_Vogt.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace _1Website_Vogt.Models {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();

        }
    }
}