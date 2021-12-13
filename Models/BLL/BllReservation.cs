using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.BLL
{
    public class BllReservation : BllBaseAbstract<reservation>
    {

        public List<reservation> GetReservations()
        {
            return GetAll(x => x.status == true);
        }

        public reservation GetReservationByTableID(int tableID)
        {
            return GetSingle(x => x.status == true && x.tableID == tableID);
        }

        public reservation GetReservationByID(int reservationID)
        {
            return GetSingle(x => x.status == true && x.ID == reservationID);
        }

        public bool SetReservation(int tableID, string name, string phoneNumber, string reservationDate, string reservationTime, int numberOfPeople)
        {
            try
            {
                return Add(new reservation
                {
                    dateTimeNow = DateTime.Now,
                    name = name,
                    numberOfPeople = numberOfPeople,
                    phoneNumber = phoneNumber,
                    reservationDate = Convert.ToDateTime(reservationDate + " " + reservationTime),
                    status = true,
                    tableID = tableID,
                    table = new BllTable().GetTableByID(tableID),
                });
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}