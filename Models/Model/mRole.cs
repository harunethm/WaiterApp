using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mRole
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int authorityLevel { get; set; }
        public bool status { get; set; }

        public mRole(role role)
        {
            ID = role.ID;
            name = role.name;
            authorityLevel = role.authorityLevel;
            status = role.status;
        }
        public mRole()
        {

        }
    }
}