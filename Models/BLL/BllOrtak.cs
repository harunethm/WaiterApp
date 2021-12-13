using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaiterApp.Models.DAL;

namespace WaiterApp.Models.BLL
{
    public class BllOrtak
    {
        public static IRepository repository = new Repository(new DBWaiterAppEntities());

        public static string QueryStringGetir(string parametre)
        {
            return HttpContext.Current.Request.QueryString[parametre];
        }

        public static object SessionDegeriGetir(string degisken)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session[degisken] == null) return null;
            else
                return HttpContext.Current.Session[degisken];
        }

        public static void SessionDegeriAta(string degisken, object deger)
        {
            HttpContext.Current.Session.Add(degisken, deger);
        }

        public static class Sessions
        {
            public static int ID
            {
                get
                {
                    var sonuc = SessionDegeriGetir("userID");
                    if (sonuc != null)
                        return Convert.ToInt32(sonuc);
                    else
                        return 0;
                }
                set
                {
                    SessionDegeriAta("userID", value);
                }
            }

            public static string name
            {
                get
                {
                    var sonuc = SessionDegeriGetir("name");
                    if (sonuc != null)
                        return Convert.ToString(sonuc);
                    else
                        return null;
                }
                set
                {
                    SessionDegeriAta("name", value);
                }
            }

            public static int roleID
            {
                get
                {
                    var sonuc = SessionDegeriGetir("roleID");
                    if (sonuc != null)
                        return Convert.ToInt32(sonuc);
                    else
                        return 0;
                }
                set
                {
                    SessionDegeriAta("roleID", value);
                }
            }

            public static void Clear()
            {
                HttpContext.Current.Session.Clear();
            }
        }
    }
}