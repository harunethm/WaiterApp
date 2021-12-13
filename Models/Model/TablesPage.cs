using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaiterApp.Models.Model
{
    public class TablesPage
    {
        public mTable table { get; set; }
        public mReservation reservation { get; set; }
        public List<mOrder> orders { get; set; }


        public TablesPage() { }
        public TablesPage(mTable table, mReservation reservation, List<mOrder> orders)
        {
            this.table = table;
            this.reservation = reservation;
            this.orders = orders;
        }
    }
}