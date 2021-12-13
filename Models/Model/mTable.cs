using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mTable
    {
        public int ID { get; set; }
        public string tableName { get; set; }
        public decimal? balance { get; set; }
        public int availability { get; set; }
        public int numberOfChairs { get; set; }
        public bool status { get; set; }

        public mTable(table table)
        {
            ID = table.ID;
            tableName = table.tableName;
            balance = table.balance;
            availability = table.availability;
            numberOfChairs = table.numberOfChairs;
            status = table.status;
        }
        public mTable()
        {

        }
    }
}