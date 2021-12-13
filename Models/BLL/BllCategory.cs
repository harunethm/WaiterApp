using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Models.BLL
{
    public class BllCategory : BllBaseAbstract<category>
    {
        public List<category> GetCategories()
        {
            return GetAll();
        }

        public List<mCategory> GetmCategories()
        {
            List<category> categoryList = GetAll();
            List<mCategory> mCategories = new List<mCategory>();
            foreach (var category in categoryList)
                mCategories.Add(new mCategory(category));
            return mCategories;
        }
    }
}