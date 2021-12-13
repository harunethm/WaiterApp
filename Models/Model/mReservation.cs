using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mReservation
    {
        public int ID { get; set; }
        public int tableID { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? dateTimeNow { get; set; }
        public DateTime reservationDate { get; set; }
        public int numberOfPeople { get; set; }
        public bool status { get; set; }

        public virtual mTable table { get; set; }

        public mReservation(reservation reservation)
        {
            ID = reservation.ID;
            tableID = reservation.tableID;
            name = reservation.name;
            phoneNumber = reservation.phoneNumber;
            dateTimeNow = reservation.dateTimeNow;
            reservationDate = reservation.reservationDate;
            numberOfPeople = reservation.numberOfPeople;
            status = reservation.status;
            table = new mTable(reservation.table);
        }
        public mReservation()
        {

        }
    }
}