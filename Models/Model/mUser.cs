using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.Model
{
    public class mUser
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string eMail { get; set; }
        public string password { get; set; }
        public string profilePicture { get; set; }
        public DateTime? registerDate { get; set; }
        public string phoneNumber { get; set; }
        public int roleID { get; set; }
        public bool status { get; set; }

        public virtual mRole role { get; set; }

        public mUser(user user)
        {
            ID = user.ID;
            name = user.name;
            surname = user.surname;
            eMail = user.eMail;
            password = user.password;
            profilePicture = user.profilePicture;
            registerDate = user.registerDate;
            phoneNumber = user.phoneNumber;
            roleID = user.roleID;
            status = user.status;

            role = new mRole(user.role);
        }
        public mUser()
        {

        }
    }
}