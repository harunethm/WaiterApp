//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WaiterApp.Models.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class reservation
    {
        public int ID { get; set; }
        public int tableID { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public Nullable<System.DateTime> dateTimeNow { get; set; }
        public System.DateTime reservationDate { get; set; }
        public int numberOfPeople { get; set; }
        public bool status { get; set; }
    
        public virtual table table { get; set; }
    }
}
