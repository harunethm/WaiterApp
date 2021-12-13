using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Models.BLL
{
    public class BllTable : BllBaseAbstract<table>
    {
        public List<table> GetTables()
        {
            return GetAll(x => x.status == true);
        }

        public table GetTableByID(int tableID)
        {
            return GetSingle(x => x.status == true && x.ID == tableID);
        }

        public bool IsTableHasOrder(int tableID) // false => sipariş yok, true => sipariş var
        {
            receipt receipt = new BllReceipt().GetReceiptByTableID(tableID);
            if (receipt != null)
            {
                List<order> orders = new BllOrder().GetOrdersByReceiptID(receipt.ID);
                if (orders != null && orders.Count > 0)
                {
                    foreach (var order in orders)
                    {
                        if (order.amount != order.paidAmount)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    new BllReceipt().CloseReceipt(receipt.ID);
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        public bool CloseTable(int tableID)
        {
            if (!IsTableHasOrder(tableID))
            {
                table table = GetTableByID(tableID);
                table.availability = 1;
                table.balance = 0;
                return Update(table);
            }
            return false;
        }

        public bool UpdateTable(mTable mTable)
        {
            table table = GetTableByID(mTable.ID);
            table.tableName = mTable.tableName;
            table.balance = mTable.balance;
            table.availability = mTable.availability;
            table.numberOfChairs = mTable.numberOfChairs;
            table.status = mTable.status;
            UpdateOnly(table);
            return true;
        }
    }
}