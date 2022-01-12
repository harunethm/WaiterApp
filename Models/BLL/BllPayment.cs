using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Models.BLL
{
    public class BllPayment : BllBaseAbstract<payment>
    {
        public payment GetPaymentByID(int ID)
        {
            return GetSingle(x => x.ID == ID);
        }

        public List<payment> GetPaymentsByOrderID(int orderID)
        {
            return GetAll(x => x.orderID == orderID);
        }


        public bool AddPayment(mPayment mPayment)
        {
            try
            {
                AddOnly(new payment
                {
                    ID = mPayment.ID,
                    dateTime = mPayment.dateTime,
                    discountAmount = mPayment.discountAmount,
                    orderID = mPayment.orderID,
                    order = new BllOrder().GetOrder(mPayment.orderID),
                    paidAmount = mPayment.paidAmount,
                    paymentMethod = mPayment.paymentMethod,
                    total = mPayment.total,
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}