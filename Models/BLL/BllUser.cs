using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.BLL
{
    public class BllUser : BllBaseAbstract<user>
    {
        public int IsUserValid(string userName, string password)
        {
            // return 0 if not valid
            // return ID if valid

            //var user = GetSingle(x => x.eMail.Equals(userName) && x.password.Equals(password));
            user user = GetSingle(x => x.status && x.eMail.StartsWith(userName) && x.eMail.EndsWith(userName) && x.password.StartsWith(password) && x.password.EndsWith(password));
            if (user != null)
                return user.ID;
            return 0;
        }

        public user GetUserByID(int ID)
        {
            return GetSingle(x => x.status == true && x.ID == ID);
        }
    }
}