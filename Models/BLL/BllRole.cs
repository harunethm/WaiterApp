using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Models.BLL
{
    public class BllRole : BllBaseAbstract<role>
    {
        public List<role> GetRoles()
        {
            return GetAll(x => x.status);
        }

        public List<role> GetAllRoles()
        {
            return GetAll();
        }

        public List<mRole> ToModel(List<role> roles)
        {
            List<mRole> mRoles = new List<mRole>();
            foreach (var role in roles)
            {
                mRoles.Add(new mRole()
                {
                    authorityLevel = role.authorityLevel,
                    ID = role.ID,
                    name = role.name,
                    status = role.status,
                });
            }
            return mRoles;
        }
    }
}