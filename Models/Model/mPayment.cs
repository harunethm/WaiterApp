using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mPayment
    {
        public int ID { get; set; }
        public int orderID { get; set; }
        public int paidAmount { get; set; }
        public int paymentMethod { get; set; }
        public decimal? discountAmount { get; set; }
        public DateTime? dateTime { get; set; }
        public decimal? total { get; set; }

        public virtual mOrder order { get; set; }

        public mPayment(payment payment)
        {
            orderID = payment.orderID;
            paidAmount = payment.paidAmount;
            paymentMethod = payment.paymentMethod;
            discountAmount = payment.discountAmount;
            dateTime = payment.dateTime;
            total = payment.total;
            order = new mOrder(payment.order);
        }
        public mPayment()
        {

        }
    }
}