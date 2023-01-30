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
    public class TableInfoController : Controller
    {
        public ActionResult Table(int id = 3)
        {
            if (id == 0)
                return RedirectToAction("TableNotFound");

            mTable mTable = new mTable();
            mReceipt mReceipt = new mReceipt();
            List<mOrder> mOrders = new List<mOrder>();
            string waiterName = "";

            table t = new BllTable().GetTableByID(id);
            if (t != null)
            {
                mTable = new mTable(t);
                receipt r = new BllReceipt().GetReceiptByTableID(id);
                if (r != null)
                {
                    waiterName = r.user.name + " " + r.user.surname;
                    mReceipt = new mReceipt(r);
                    List<order> o = new BllOrder().GetOrdersByReceiptID(r.ID);
                    if (o != null)
                    {
                        mOrders = new BllOrder().ToModel(o);
                    }
                }
            }

            ViewBag.orders = mOrders;
            ViewBag.table = mTable;
            ViewBag.receipt = mReceipt;
            ViewBag.waiter = waiterName;
            return View();
        }

        public ActionResult TableNotFound()
        {
            return View();
        }

    }
}