using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WaiterApp.Models.BLL;

namespace WaiterApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            BllOrtak.Sessions.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName, string password, string rememberMe)
        {
            BllUser blluser = new BllUser();
            int ID = blluser.IsUserValid(@userName, @password);
            if (ID > 0)
            {
                BllOrtak.Sessions.ID = ID;
                BllOrtak.Sessions.name = userName;
                BllOrtak.Sessions.roleID = blluser.GetUserByID(ID).roleID;
                FormsAuthentication.SetAuthCookie(ID.ToString(), rememberMe == "on" ? true : false);
                return RedirectToAction("Menu", "Home");
            }
            return View(new { error = "Kullanıcı bulunamadı." });
        }


        public JsonResult LogOut()
        {
            BllOrtak.Sessions.Clear();
            FormsAuthentication.SignOut();
            return Json(new { confirm = true }, JsonRequestBehavior.AllowGet);
        }

    }
}