using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mOrder
    {
        public int ID { get; set; }
        public int receiptID { get; set; }
        public int productID { get; set; }
        public int amount { get; set; }
        public int paidAmount { get; set; }
        public string comment { get; set; }
        //1 = hazırlanıyor, 2 = hazır, 3 = ödendi, 4 = iptal
        public int status { get; set; }

        public virtual mProduct product { get; set; }
        public virtual mReceipt receipt { get; set; }

        public mOrder(order order)
        {
            ID = order.ID;
            receiptID = order.receiptID;
            productID = order.productID;
            amount = order.amount;
            paidAmount = order.paidAmount;
            comment = order.comment;
            status = order.status;
            product = new mProduct(order.product);
            receipt = new mReceipt(order.receipt);
        }

        public mOrder()
        {

        }
    }
}