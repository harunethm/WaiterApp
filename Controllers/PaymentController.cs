using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaiterApp.Models.BLL;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        [HttpGet]
        public ActionResult Index(int tableID = 0)
        {
            if (tableID > 0)
            {
                table _table = new BllTable().GetTableByID(tableID);

                if (_table != null && _table.availability > 1)
                {
                    List<mOrder> orders = new BllOrder().GetmOrdersByTableID(tableID).Where(x => x.status == 2).ToList();
                    foreach (var order in orders)
                        order.receipt = null;
                    ViewBag.orders = orders;
                    ViewBag.jsonOrders = JsonConvert.SerializeObject(orders);
                    ViewBag.user = new mUser(new BllUser().GetUserByID(BllOrtak.Sessions.ID));
                    return View();
                }
            }
            return RedirectToAction("Masalar", "Home");
        }

        public JsonResult Pay(string sepet = "", bool isCash = false, bool isCreditCard = false, string discount = "")
        {
            mOrder mOrder = new mOrder();
            mPayment mPayment = new mPayment();
            mTable mTable = new mTable();

            BllOrder bllOrder = new BllOrder();
            BllPayment bllPayment = new BllPayment();
            BllTable bllTable = new BllTable();
            BllReceipt bllReceipt = new BllReceipt();

            // TODO eğer tüm siparişler ödendiyse masayı ve receipt ı kapat


            try
            {
                List<mSepet> mSepet = JsonConvert.DeserializeObject<mSepet[]>(sepet).ToList();
                foreach (var item in mSepet.GroupBy(x => x.orderID))
                {
                    int orderID = item.FirstOrDefault().orderID;
                    int paid = item.Count();
                    decimal discountAmount = Convert.ToDecimal(discount);
                    decimal total = paid * item.FirstOrDefault().product.price;

                    //if (discountAmount > total)
                    //    discountAmount = total;

                    mOrder = new mOrder(bllOrder.GetOrder(orderID));

                    mOrder.paidAmount += paid;
                    if (mOrder.amount == mOrder.paidAmount)
                        mOrder.status = 3;

                    mPayment = new mPayment
                    {
                        orderID = item.FirstOrDefault().orderID,
                        paidAmount = paid,
                        paymentMethod = isCreditCard ? 1 : isCash ? 2 : 0,
                        discountAmount = discountAmount,
                        dateTime = DateTime.Now,
                        total = total - discountAmount,
                    };

                    mTable = mOrder.receipt.table;
                    mTable.balance -= total;
                    
                    if(mTable.balance == 0)
                        mTable.availability = 1;
                    
                    if (bllOrder.isAllPaid(mOrder.receiptID))
                        bllReceipt.CloseReceipt(mOrder.receiptID);

                    bool updateTable = bllTable.UpdateTable(mTable);
                    bool updateOrder = bllOrder.UpdateOrder(mOrder);
                    bool addPayment = bllPayment.AddPayment(mPayment);

                    if (updateOrder && addPayment && updateTable)
                    { bllOrder.Save(); bllPayment.Save(); bllTable.Save(); }
                }

                return Json(new { confirm = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { confirm = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}