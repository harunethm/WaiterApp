using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaiterApp.Models.BLL;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            int ID = BllOrtak.Sessions.ID;
            if (ID > 0)
                ViewBag.user = new mUser(new BllUser().GetUserByID(ID));

            return View();
        }

        public JsonResult EditProfile(string name, string surName, string eMail, string phoneNumber, string password, string passwordRepeat)
        {
            if (password.Equals(passwordRepeat))
            {
                BllUser bllUser = new BllUser();
                user user = bllUser.GetUserByID(BllOrtak.Sessions.ID);
                user.name = name;
                user.surname = surName;
                user.eMail = eMail;
                user.phoneNumber = phoneNumber;
                user.password = password;

                return Json(new { confirm = bllUser.Update(user) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { confirm = false }, JsonRequestBehavior.AllowGet);
        }

    }
}