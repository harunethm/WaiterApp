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
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Menu()
        {
            try
            {
                List<mMenu> menu = new List<mMenu>();
                BllCategory bllCategory = new BllCategory();
                BllProduct bllProduct = new BllProduct();

                List<category> categories = bllCategory.GetCategories();
                if (categories != null && categories.Count > 0)
                {
                    foreach (category category in categories)
                    {
                        List<product> products = bllProduct.GetProductsByCategory(categoryID: category.ID);
                        if (products != null && products.Count > 0)
                        {
                            menu.Add(new mMenu()
                            {
                                category = new mCategory(category),
                                products = bllProduct.ToModel(products),
                            });
                        }
                    }
                }

                ViewBag.user = new mUser(new BllUser().GetUserByID(BllOrtak.Sessions.ID));
                ViewBag.menu = menu;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult Masalar()
        {
            try
            {
                List<TablesPage> tps = new List<TablesPage>();
                foreach (var table in new BllTable().GetTables())
                {
                    mTable mTable = new mTable(table);

                    mReservation mReservation = new mReservation();
                    reservation reservation = new BllReservation().GetReservationByTableID(table.ID);
                    if (reservation != null)
                        mReservation = new mReservation(reservation);

                    List<mOrder> mOrders = new List<mOrder>();
                    List<order> orders = new BllOrder().GetOrdersByTableID(table.ID);
                    if (orders != null)
                        foreach (var order in orders)
                            mOrders.Add(new mOrder(order));

                    tps.Add(new TablesPage
                    {
                        table = mTable,
                        reservation = mReservation,
                        orders = mOrders,
                    });
                }

                ViewBag.user = new mUser(new BllUser().GetUserByID(BllOrtak.Sessions.ID));
                ViewBag.TablesPages = tps;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Masalar(int tableID)
        {
            if (tableID > 0)
            {
                return RedirectToAction("Index", "Payment", new { tableID = tableID }); ;
            }
            return View();
        }

        public JsonResult MasayiKapat(int tableID)
        {
            BllTable bllTable = new BllTable();

            bool isSuccess = bllTable.CloseTable(tableID);

            return Json(new { confirm = isSuccess }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Siparisler()
        {
            try
            {
                BllOrder bllOrder = new BllOrder();
                List<order> orders = bllOrder.GetOrdersForOrdersPage();

                ViewBag.user = new mUser(new BllUser().GetUserByID(BllOrtak.Sessions.ID));
                ViewBag.orders = orders;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public JsonResult RezerveEt(int tableID, string name, string phoneNumber, string reservationDate, string reservationTime, int numberOfPeople)
        {
            // TODO rezerve edilen masa için receipt oluşturmak gerekebilir
            try
            {
                BllReservation bllReservation = new BllReservation();
                BllTable bllTable = new BllTable();
                bool reservationSuccess = false;
                bool tableUpdateSuccess = false;
                if (tableID > 0 && !name.IsNullOrEmpty() && !phoneNumber.IsNullOrEmpty() && !reservationDate.IsNullOrEmpty() && !reservationTime.IsNullOrEmpty() && numberOfPeople > 0)
                {
                    if (reservationTime.ToDateTime() < DateTime.Now.AddMinutes(60))
                    {
                        return Json(new { confirm = false, errorMessage = "Geçmiş tarihli rezervasyon yapılamaz. Lütfen gelecek bir zamanı seçin. (Rezervasyon en az 1 saat önceden yapılmalıdır.)" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        reservationSuccess = bllReservation.SetReservation(
                        tableID: tableID,
                        name: name,
                        phoneNumber: phoneNumber,
                        reservationDate: reservationDate,
                        reservationTime: reservationTime,
                        numberOfPeople: numberOfPeople
                        );
                        table table = bllTable.GetTableByID(tableID);
                        table.availability = 3;
                        tableUpdateSuccess = bllTable.Update(table);
                    }
                }
                else
                {
                    return Json(new { confirm = false, errorMessage = "Lütfen tüm alanları eksiksiz doldurduğunuzdan emin olunuz." }, JsonRequestBehavior.AllowGet);
                }

                if (reservationSuccess && tableUpdateSuccess)
                {
                    return Json(new { confirm = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { confirm = false, errorMessage = "İşlem sırasında bir hata oluştu." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { confirm = false, errorMessage = "Bir hata oldu." }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult RezervasyonIptal(int reservationID)
        {
            try
            {
                if (reservationID > 0)
                {
                    BllReservation bllReservation = new BllReservation();
                    BllTable bllTable = new BllTable();

                    reservation reservation = bllReservation.GetReservationByID(reservationID);
                    reservation.status = false;

                    table table = bllTable.GetTableByID(reservation.tableID);
                    table.availability = 1;
                    // TODO eğer masa için receipt açıldıysa onun da silinmesi gerekli

                    bllReservation.Update(reservation);
                    bllTable.Update(table);

                    return Json(new { confirm = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { confirm = false, errorMessage = "Bir hata oldu." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { confirm = false, errorMessage = "Bir hata oldu." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}