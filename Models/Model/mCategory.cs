using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mCategory
    {

        public int ID { get; set; }
        public string name { get; set; }
        public bool status { get; set; }

        public mCategory()
        {

        }

        public mCategory(category category)
        {
            ID = category.ID;
            name = category.name;
            status = category.status;
        }
    }
}