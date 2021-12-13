using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Models.BLL
{
    public class BllProduct : BllBaseAbstract<product>
    {
        public List<product> GetProducts()
        {
            return GetAll(x => x.status == true);
        }

        public product GetProducts(int productID)
        {
            return GetAll(x => x.status == true && x.ID == productID).FirstOrDefault();
        }

        public List<product> GetProductsByCategory(int categoryID)
        {
            return GetAll(x => x.status == true && x.categoryID == categoryID);
        }

        public product GetProductByOrderID(int orderID)
        {
            return new BllOrder().GetOrder(orderID).product;
        }

        public List<mProduct> GetmProductsByCategory(int categoryID)
        {
            List<product> products = GetProductsByCategory(categoryID);
            return ToModel(products);
        }

        public mProduct GetmProductByOrderID(int orderID)
        {
            return new BllOrder().GetmOrder(orderID).product;
        }

        public List<mProduct> ToModel(List<product> list)
        {
            List<mProduct> model = new List<mProduct>();
            foreach (var index in list)
                model.Add(new mProduct(index));
            return model;
        }
    }
}