using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;
using WaiterApp.Models.Model;

namespace WaiterApp.Models.BLL
{
    public class BllUser : BllBaseAbstract<user>
    {
        public int IsUserValid(string userName = "", string password = "")
        {
            // return 0 if not valid
            // return ID if valid
            user user = new user();
            if (userName != "" & password != "")
                user = GetSingle(x => x.status && x.eMail.StartsWith(userName) && x.eMail.EndsWith(userName) && x.password.StartsWith(password) && x.password.EndsWith(password));
            if (user != null)
                return user.ID;
            return 0;
        }

        public user GetUserByID(int ID)
        {
            return GetSingle(x => x.ID == ID);
        }

        public List<mUser> GetmUsers()
        {
            return ToModel(GetAll());
        }

        public bool AddUser(mUser mUser)
        {
            if (mUser.name.IsNullOrEmpty())
                return false;
            if (mUser.surname.IsNullOrEmpty())
                return false;
            if (mUser.eMail.IsNullOrEmpty())
                return false;
            if (mUser.phoneNumber.IsNullOrEmpty())
                return false;
            if (mUser.password.IsNullOrEmpty())
                return false;
            if (mUser.roleID == 0)
                return false;

            // mUser != null
            return Add(new user()
            {
                name = mUser.name,
                surname = mUser.surname,
                phoneNumber = mUser.phoneNumber,
                eMail = mUser.eMail,
                password = mUser.password,
                registerDate = DateTime.Now,
                roleID = mUser.roleID,
                status = true,
                profilePicture = /*mUser.profilePicture*/ @"\Belgeler\Images\bg1a.jpg",
            });
        }


        public List<mUser> ToModel(List<user> users)
        {
            List<mUser> mUsers = new List<mUser>();
            foreach (var user in users)
            {
                mUsers.Add(new mUser()
                {
                    eMail = user.eMail,
                    ID = user.ID,
                    name = user.name,
                    password = user.password,
                    phoneNumber = user.phoneNumber,
                    profilePicture = user.profilePicture,
                    registerDate = user.registerDate,
                    roleID = user.roleID,
                    role = new mRole(user.role),
                    surname = user.surname,
                    status = user.status,
                });
            }
            return mUsers;
        }
    }
}