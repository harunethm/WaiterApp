using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mReceipt
    {
        public int ID { get; set; }
        public int tableID { get; set; }
        public int waiterID { get; set; }
        public DateTime? openingTime { get; set; }
        public DateTime? closingTime { get; set; }
        public bool status { get; set; }

        public virtual mUser user { get; set; }
        public virtual mTable table { get; set; }


        public mReceipt(receipt receipt)
        {
            ID = receipt.ID;
            tableID = receipt.tableID;
            waiterID = receipt.waiterID;
            openingTime = receipt.openingTime;
            closingTime = receipt.closingTime;
            status = receipt.status;
            user = new mUser(receipt.user);
            table = new mTable(receipt.table);
        }
        public mReceipt()
        {

        }
    }
}