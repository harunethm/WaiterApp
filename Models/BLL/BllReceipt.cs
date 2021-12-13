using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.BLL
{
    public class BllReceipt: BllBaseAbstract<receipt>
    {
        public receipt GetReceiptByTableID(int tableID)
        {
            return GetSingle(x => x.status == true && x.tableID == tableID);
        }

        public bool CloseReceipt(int receiptID)
        {
            receipt receipt = GetSingle(x => x.ID == receiptID);
            receipt.closingTime = DateTime.Now;
            receipt.status = false;
            return Update(receipt);
        }
    }
}