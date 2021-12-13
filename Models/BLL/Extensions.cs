using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WaiterApp.Models.BLL
{
    public static class Extensions
    {
        public static int? ToInt32(this string deger)
        {
            if (deger == null && deger.IsNullOrEmpty())
                return null;
            try
            {
                return Convert.ToInt32(deger);
            }
            catch
            {
                return null;
            }
        }

        public static Int64? ToInt64(this string deger)
        {
            if (deger == null && deger.IsNullOrEmpty())
                return null;
            try
            {
                return Convert.ToInt64(deger);
            }
            catch
            {
                return null;
            }
        }

        public static byte ToByte(this string deger)
        {
            return Convert.ToByte(deger);
        }

        public static double? ToDouble(this string deger)
        {
            if (deger == null && deger.IsNullOrEmpty())
                return null;
            try
            {
                return Convert.ToDouble(deger);
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ToDecimal(this string deger)
        {
            if (deger == null && deger.IsNullOrEmpty())
                return null;
            try
            {
                return Convert.ToDecimal(deger);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? ToDateTime(this string deger)
        {
            if (deger == null && deger.IsNullOrEmpty())
                return null;
            try
            {
                return DateTime.Parse(deger);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsNullOrEmpty(this string veri)
        {
            if (veri == null)
                return true;
            return (string.IsNullOrEmpty(veri.Trim()));
        }

        public static string ToSqlServerDateTimeString(this DateTime datetime, bool? baslangicTarihimi = null)
        {
            if (datetime == null) return string.Empty;
            else
            {
                if (baslangicTarihimi == null)
                {
                    return
                    datetime.Year + "-" + datetime.Month + "-" + datetime.Day + " " + datetime.Hour + ":" + datetime.Minute + ":" + datetime.Second;
                }
                else if (baslangicTarihimi == true)
                {
                    return
                    datetime.Year + "-" + datetime.Month + "-" + datetime.Day + " 00:00:00";
                }
                else
                {
                    return
                    datetime.Year + "-" + datetime.Month + "-" + datetime.Day + " 23:59:59";
                }

            }
        }

        public static string ToTitleCase(this string metin)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(metin.ToLower());
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static T ConvertToClass<T>(this Object myobj)
        {
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            if (myobj == null)
                return (T)x;
            Type objectType = myobj.GetType();

            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                try
                {
                    propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                    value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                    propertyInfo.SetValue(x, value, null);
                }
                catch
                {

                }
            }
            return (T)x;
        }
        public static List<T> ConvertToList<T, U>(this List<U> myobj)
        {
            List<T> result = new List<T>();
            foreach (object item in myobj)
            {
                try
                {
                    Type objectType = item.GetType();
                    Type target = typeof(T);
                    var x = Activator.CreateInstance(target, false);
                    var z = from source in objectType.GetMembers().ToList()
                            where source.MemberType == MemberTypes.Property
                            select source;
                    var d = from source in target.GetMembers().ToList()
                            where source.MemberType == MemberTypes.Property
                            select source;
                    List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
                       .ToList().Contains(memberInfo.Name)).ToList();
                    PropertyInfo propertyInfo;
                    object value;
                    foreach (var memberInfo in members)
                    {
                        try
                        {
                            propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                            value = item.GetType().GetProperty(memberInfo.Name).GetValue(item, null);

                            propertyInfo.SetValue(x, value, null);
                        }
                        catch
                        {
                        }

                    }
                    result.Add((T)x);
                }
                catch
                {
                }
            }
            return result;
        }
    }
}