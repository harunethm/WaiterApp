using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WaiterApp.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index(string text)
        {
            ViewBag.text = text;
            return View();
        }
    }
}