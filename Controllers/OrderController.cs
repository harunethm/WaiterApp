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
    public class OrderController : Controller
    {
        public JsonResult SiparisIptal(int orderID, int productID)
        {
            try
            {
                BllOrder bllOrder = new BllOrder();
                List<order> orders = bllOrder.GetOrdersByOrderIDandProductID(orderID, productID);
                foreach (var order in orders)
                {
                    order.status = false;
                    bllOrder.Update(order);
                }
                return Json(new { confirm = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { confirm = false, errorMessage = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SiparisDuzenle(int orderID, int productID, int newAmount)
        {
            try
            {
                BllOrder bllOrder = new BllOrder();
                List<order> orders = bllOrder.GetOrdersByOrderIDandProductID(orderID, productID);
                if (orders.Count > 1)
                    return Json(new { confirm = false, errorMessage = "kriterlere uyan birden fazla ürün var !!!" }, JsonRequestBehavior.AllowGet);
                else
                {
                    orders.FirstOrDefault().amount = newAmount + orders.FirstOrDefault().paidAmount;
                    bllOrder.Update(orders.FirstOrDefault());
                }

                return Json(new { confirm = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { confirm = false, errorMessage = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}