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
        public category GetCategory(int ID)
        {
            return GetSingle(x => x.ID == ID);
        }

        public List<category> GetCategories()
        {
            return GetAll(x => x.status);
        }

        public List<category> GetAllCategories()
        {
            return GetAll();
        }

        public List<mCategory> GetmCategories()
        {
            List<category> categoryList = GetCategories();
            if (categoryList != null)
                return ToModel(categoryList);
            return null;
        }

        public List<mCategory> GetAllmCategories()
        {
            List<category> categoryList = GetAllCategories();
            if (categoryList != null)
                return ToModel(categoryList);
            return null;
        }

        public List<mCategory> ToModel(List<category> categoryList)
        {
            List<mCategory> mCategories = new List<mCategory>();
            foreach (var category in categoryList)
                mCategories.Add(new mCategory(category));
            return mCategories;
        }

        public bool UpdateCategory(int ID, string name, bool status)
        {
            category category = GetSingle(x => x.ID == ID);
            category.name = name;
            category.status = status;
            return Update(category);
        }

        public bool AddCategory(string name)
        {
            category category = new category();
            category.name = name;
            category.status = true;
            return Add(category);
        }
    }
}