using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniFinance.Common
{
    public static class ExtensionMethods
    {
        public static string ToNull(this string obj)
        {
            if (obj.Equals(""))
            {
                return null;
            }

            return obj;
        }

        public static double? ToDoubleNullable(this Object obj)
        {
            if (obj.IsNull())
            {
                return null;
            }

            return double.Parse(obj.ToString());
        }

        public static Object DBNullToNull(this Object obj)
        {
            if (obj == DBNull.Value)
            {
                return null;
            }

            return obj;
        }

        public static Object ToDBNull(this Object obj)
        {
            if (obj == null || obj.Equals(""))
            {
                return DBNull.Value;
            }
            if (obj is DateTime && ((DateTime)obj).Equals(DateTime.MinValue))
            {
                return DBNull.Value;
            }

            return obj;
        }

        public static bool IsNull(this object obj)
        {

            return (obj == null || obj == DBNull.Value);
        }

        //public static bool IsNullOrEmpty(this string obj)
        //{
        //    if (obj == null || obj.Equals(""))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return true;
            }
            if (obj.ToString().Equals(""))
            {
                return true;
            }

            return false;
        }



        //public static string ToMD5(this string obj)
        //{
        //    if (obj != null && !obj.Equals(""))
        //    {
        //        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(obj, "MD5");
        //    }

        //    return obj;
        //}

        public static long ToLong(this string obj)
        {
            try
            {
                if (!obj.IsNull())
                {
                    return long.Parse(obj.ToString());
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static int ToInt(this string obj)
        {
            try
            {
                if (!obj.IsNull())
                {
                    return int.Parse(obj.ToString());
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static Int16 ToInt(this short obj)
        {
            try
            {
                if (!obj.IsNull())
                {
                    return Int16.Parse(obj.ToString());
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static Int16 ToInt(this Int32 obj)
        {
            try
            {
                if (!obj.IsNull())
                {
                    return Int16.Parse(obj.ToString());
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static decimal ToDecimal(this string obj)
        {
            try
            {
                if (!obj.IsNull())
                {
                    return decimal.Parse(obj.ToString());
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime FirstDayOfMonth(this DateTime obj)
        {
            try
            {
                if (!obj.IsNull())
                    return DateTime.Parse(obj.AddDays(-(obj.Day - 1)).ToShortDateString().Trim() + " 00:00:00");
                else
                    return DateTime.Parse(obj.ToShortDateString().Trim() + " 00:00:00");
            }
            catch
            {

                throw;
            }
        }

        public static DateTime LastDayOfMonth(this DateTime obj)
        {
            try
            {
                if (!obj.IsNull())
                    return DateTime.Parse(obj.AddMonths(1).FirstDayOfMonth().AddDays(-1).ToShortDateString().Trim() + " 00:00:00");
                else
                    return DateTime.Parse(obj.ToShortDateString().Trim() + " 00:00:00");
            }
            catch
            {

                throw;
            }
        }

        public static string AddSingleQuote(this string obj)
        {
            try
            {
                return string.Format("'{0}'", obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Object ToEmptyString(this Object obj)
        {
            if (obj == null)
            {
                return "";
            }

            return obj;
        }

        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            int index = 0;
            while (index + maxLength < str.Length)
            {
                yield return str.Substring(index, maxLength);
                index += maxLength;
            }

            yield return str.Substring(index);
        }

        //public static string TextThaiBath(this decimal obj)
        //{
        //    string toReturn = "";
        //    string[] sNumber = { "", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };
        //    string[] sDigit = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน" };
        //    string[] sDigit10 = { "", "สิบ", "ยี่สิบ", "สามสิบ", "สี่สิบ", "ห้าสิบ", "หกสิบ", "เจ็ดสิบ", "แปดสิบ", "เก้าสิบ" };
        //    dynamic nLen = null;
        //    string sWord = null;
        //    string sByte1 = null;
        //    int i = 0, j = 0;
        //    string sNum = obj.ToString("n2").Replace(",", "");
        //    nLen = sNum.Length;
        //    if (sNum == "0.00")
        //        toReturn = "ศูนย์";

        //    for (i = 1; i <= nLen - 3; i++)
        //    {
        //        j = (15 + nLen - (i)) % 6;
        //        sByte1 = sNum.Substring(i - 1, 1);

        //        if (sByte1 != "0")
        //        {
        //            if (j == 1)
        //                sWord = sDigit10[UtilityCls.GetInt(sByte1)];
        //            else
        //                sWord = sNumber[UtilityCls.GetInt(sByte1)] + sDigit[j];
        //            toReturn += sWord;
        //        }
        //        if ((j == 0) && (i != nLen - 3))
        //        {
        //            toReturn = toReturn + "ล้าน";
        //            toReturn = toReturn.Replace("หนึ่งล้าน", "เอ็ดล้าน");
        //        }
        //    }
        //    if (sNum.Substring(0, 1) == "1")
        //        toReturn = toReturn.Replace("เอ็ดล้าน", "หนึ่งล้าน");
        //    if (sNum.Substring(0, 2) == "11")
        //        toReturn = toReturn.Replace("สิบหนึ่งล้าน", "สิบเอ็ดล้าน");
        //    if (toReturn.Length > 0)
        //        toReturn = toReturn + "บาท";
        //    if (nLen > 4)
        //        toReturn = toReturn.Replace("หนึ่งบาท", "เอ็ดบาท");
        //    sNum = sNum.Substring(sNum.Length - 2);
        //    if (sNum == "00")
        //    {
        //        toReturn = toReturn + "ถ้วน";
        //    }
        //    else
        //    {
        //        if (sNum.Substring(0, 1) != "0")
        //            toReturn = toReturn + sDigit10[UtilityCls.GetInt(sNum.Substring(0, 1))];
        //        if (sNum.Substring(sNum.Length - 1) != "0")
        //            toReturn = toReturn + sNumber[UtilityCls.GetInt(sNum.Substring(sNum.Length - 1))];
        //        toReturn = toReturn + "สตางค์";
        //        if (sNum.Substring(0, 1) != "0")
        //            toReturn = toReturn.Replace("หนึ่งสตางค์", "เอ็ดสตางค์");
        //    }
        //    return toReturn;
        //}
        public static string ToStringOrEmpty(this Object obj)
        {
            try
            {
                return obj.ToString().Trim();
            }
            catch
            {
                return "";
            }
        }

        public static bool IsTextFile(this string filename)
        {
            try
            {
                string strFileExtension = System.IO.Path.GetExtension(filename).ToLower();
                if (strFileExtension == Constants.FilterExtFile.TEXT)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public static bool IsExcelFile(this string filename)
        {
            try
            {
                string strFileExtension = System.IO.Path.GetExtension(filename).ToLower();
                if (strFileExtension == Constants.FilterExtFile.EXCEL_XLSX)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
        //public static int GetColumnByName(this OfficeOpenXml.ExcelWorksheet ws, string columnName)
        //{
        //    if (ws == null) throw new ArgumentNullException(nameof(ws));
        //    return ws.Cells["1:1"].First(c => c.Value.ToString() == columnName).Start.Column;
        //}
        //public static bool IsBlankRow(this OfficeOpenXml.ExcelWorksheet ws, int iRow)
        //{
        //    if (ws == null) throw new ArgumentNullException(nameof(ws));
        //    if (ws.Cells[iRow.ToString() + ":" + iRow.ToString()].Any(x => !(x.Value.IsNullOrEmpty())))
        //        return false;
        //    return true;
        //}
        public static T Clone<T>(this T obj)
        {
            var inst = obj.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            return (T)inst?.Invoke(obj, null);
        }

        public static TEntity ShallowCopyEntity<TEntity>(TEntity source) where TEntity : class, new()
        {

            // Get properties from EF that are read/write and not marked witht he NotMappedAttribute
            var sourceProperties = typeof(TEntity)
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite &&
                            p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.CompareAttribute), true).Length == 0);
            var newObj = new TEntity();

            foreach (var property in sourceProperties)
            {

                // Copy value
                property.SetValue(newObj, property.GetValue(source, null), null);

            }

            return newObj;

        }
    }
}