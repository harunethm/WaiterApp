using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mTakeAway
    {
        public int ID { get; set; }
        public int receiptID { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string phoneNumber { get; set; }
        public string customerAdress { get; set; }
        public bool status { get; set; }

        public virtual mReceipt receipt { get; set; }

        public mTakeAway(takeAway takeAway)
        {
            ID = takeAway.ID;
            receiptID = takeAway.receiptID;
            customerName = takeAway.customerName;
            customerSurname = takeAway.customerSurname;
            phoneNumber = takeAway.phoneNumber;
            customerAdress = takeAway.customerAdress;
            status = takeAway.status;
            receipt = new mReceipt(takeAway.receipt);
        }
        public mTakeAway()
        {

        }
    }
}