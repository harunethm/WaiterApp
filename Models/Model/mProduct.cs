using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mProduct
    {
        public int ID { get; set; }
        public int categoryID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public bool status { get; set; }

        public mCategory category { get; set; }


        public mProduct(product product)
        {
            ID = product.ID;
            categoryID = product.categoryID;
            name = product.name;
            description = product.description;
            price = product.price;
            status = product.status;
            category = new mCategory(product.category);
        }
        public mProduct()
        {

        }
    }
}