using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Models.BLL
{
    public class BllOrder : BllBaseAbstract<order>
    {
        public List<order> GetOrders()
        {
            return GetAll();
        }

        public List<order> GetOrdersForOrdersPage()
        {
            return GetAll(x => x.receipt.status && (x.status == 1 || x.status == 2));
        }

        public order GetOrder(int orderID)
        {
            return GetAll(x => x.ID == orderID).FirstOrDefault();
        }

        public List<order> GetOrdersByOrderIDandProductID(int orderID, int productID)
        {
            return GetAll(x => x.ID == orderID && x.productID == productID);
        }

        public List<order> GetOrdersByReceiptID(int receiptID)
        {
            return GetAll(x => x.receiptID == receiptID);
        }
        public List<order> GetOrdersByTableID(int tableID)
        {
            receipt receipt = new BllReceipt().GetReceiptByTableID(tableID);
            if (receipt != null)
                return GetOrdersByReceiptID(receipt.ID);
            else
                return null;
        }

        public List<mOrder> GetmOrders()
        {
            return ToModel(GetOrders());
        }
        public mOrder GetmOrder(int orderID)
        {
            return new mOrder(GetOrder(orderID));
        }
        public List<mOrder> GetmOrdersByTableID(int tableID)
        {
            receipt receipt = new BllReceipt().GetReceiptByTableID(tableID);
            List<order> orders = GetOrdersByReceiptID(receipt.ID);
            return ToModel(orders);
        }
        public bool isAllPaid(int receiptID)
        {
            List<order> orders = GetOrdersByReceiptID(receiptID);
            foreach (var item in orders)
            {
                if(item.status == 1 || item.status == 2)
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdateOrder(mOrder mOrder)
        {
            try
            {
                order order = new BllOrder().GetOrder(mOrder.ID);
                order.amount = mOrder.amount;
                order.comment = mOrder.comment;
                order.paidAmount = mOrder.paidAmount;
                order.payments = new BllPayment().GetPaymentsByOrderID(mOrder.ID);
                order.product = new BllProduct().GetSingle(x => x.ID == mOrder.product.ID);
                order.productID = mOrder.productID;
                order.receipt = new BllReceipt().GetSingle(x => x.ID == mOrder.receipt.ID);
                order.status = mOrder.status;
                UpdateOnly(order);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<mOrder> ToModel(List<order> orders)
        {
            List<mOrder> mOrders = new List<mOrder>();
            foreach (var order in orders)
                mOrders.Add(new mOrder(order));
            return mOrders;
        }
    }
}