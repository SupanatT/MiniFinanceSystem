using MiniFinance.Models.DataModel;
using MiniFinance.ViewModel.ReportCloseDate_Detail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MiniFinance.Common
{
    public static class CommonMethods
    {
        private static readonly ACCOUNTINGEntities _dbCommonMethods = new ACCOUNTINGEntities();
        

        public static string GetFormatTextProductTrans(string AssetType, bool haveIndex, string index)
        {
            string toReturn = "";
            try
            {
                toReturn = "ค่าสินค้า (" + GetMasterDescription(AssetType, Constants.MasterType.AssetType) + ")";

                if(AssetType == Constants.AssetType.Other && haveIndex == true)
                {
                    toReturn += " #" + index;
                }

                return toReturn;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static string GetCustomerFullName(string CustCode)
        {
            string toReturn = "";
            try
            {
                var obj = _dbCommonMethods.VIEW_M_CUST_CUSTOMER.FirstOrDefault(x => x.CUSTOMER_CODE == CustCode);
                if(obj != null)
                {
                    toReturn = GetMasterDescription(obj.TITLE_ID, Constants.MasterType.CustomerTitle) + " " + obj.FIRST_NAME + " " + obj.LAST_NAME;
                }

                return toReturn;

            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static decimal GetVatRate()
        {
            try
            {
                //รับค่า Rate % จาก Common
                var objRate = _dbCommonMethods.VIEW_M_RATE.FirstOrDefault(x => (x.EFFECT_DATE <= DateTime.Now && x.RATE_TYPE == "Vat"));
                if (!objRate.IsNullOrEmpty())
                    return objRate.RATE;
                return 0;

            }
            catch (Exception e)
            {

                throw;
            }
        }
        public static string GetMasterDescription(string code, string type)
        {
            try
            {
                string result = "";
                var model = _dbCommonMethods.VIEW_M_MASTER.FirstOrDefault(x => x.MASTER_ID == code && x.MASTER_TYPE_ID == type);
                result = (model != null) ? model.NAME_THA : code;
                if (result.IsNullOrEmpty())
                    return code;
                return result;
            }
            catch (Exception e)
            {
                return code;
            }
        }

        
        public static string GetMonthText(int iMonth)
        {
            var months = new Dictionary<int, String>();
            months.Add(1, "มกราคม");
            months.Add(2, "กุมภาพันธ์");
            months.Add(3, "มีนาคม");
            months.Add(4, "เมษายน");
            months.Add(5, "พฤษภาคม");
            months.Add(6, "มิถุนายน");
            months.Add(7, "กรกฎาคม");
            months.Add(8, "สิงหาคม");
            months.Add(9, "กันยายน");
            months.Add(10, "ตุลาคม");
            months.Add(11, "พฤศจิกายน");
            months.Add(12, "ธันวาคม");
            var returnVal = months.Where(x => x.Key == iMonth).Select(x=> x.Value).Single();
            return returnVal;
        }


        public static string GetTransCodeDescription(string TransCode)
        {
            try
            {//
                return (_dbCommonMethods.VIEW_M_TRANS_CODE.FirstOrDefault(x => x.TRANS_CODE == TransCode)?.DESCRIPTION_THA) ?? "";
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static string GetSysConfigValue(string SysconfigType, string SysconfigID)
        {
            try
            {
                var obj = _dbCommonMethods.VIEW_M_SYSCONFIG.FirstOrDefault(x => x.SYSCONFIG_TYPE == SysconfigType && x.SYSCONFIG_ID == SysconfigID);
                return obj?.VALUE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static bool checkCompanyGroup(string CompGroupCode, string CompCode)
        {
            try
            {
                List<string> lstCompInGroup = new List<string>();
                var obj = _dbCommonMethods.VIEW_M_COMPANY.Where(x => x.COMPANY_GROUP == CompGroupCode).ToList();

                foreach (var item in obj)
                {
                    lstCompInGroup.Add(item.COMPANY_CODE);
                }

                return lstCompInGroup.Contains(CompCode);

            }
            catch (Exception e)
            {
                throw;
            }
        }
      
        public static string getAssetDescription(int? AssetID, string AssetType)
        {
            string toReturn = "";
           
            try
            {
                if (AssetID != null && AssetID != 0)
                {
                    if (AssetType == Constants.AssetType.Vehicle)
                    {
                        var vehicle = (from a in _dbCommonMethods.VIEW_FRONTEND_M_ASSET_VEHICLE
                                       join b in _dbCommonMethods.VIEW_FRONTEND_M_BRAND on a.BRAND_ID equals b.BRAND_ID into g1
                                       from b in g1.DefaultIfEmpty()

                                       join c in _dbCommonMethods.VIEW_FRONTEND_M_MODEL on a.MODEL_ID equals c.MODEL_ID into g2
                                       from c in g2.DefaultIfEmpty()

                                       where a.ASSET_ID == AssetID
                                       select new { a, b, c }).FirstOrDefault();
                        if (vehicle != null)
                        {
                            toReturn += "ยี่ห้อรถ = '" + vehicle.b?.NAME_THA + "', รุ่นรถ = '" + vehicle.c?.NAME_THA + "',<br/>";
                            toReturn += "เลขเครื่องยนต์ = '" + vehicle.a.ENGINE_NO + "', <br/>";
                            toReturn += "เลขตัวถังรถ = '" + vehicle.a.CHASSIS_NO + "'";
                        }
                    }
                    else if (AssetType == Constants.AssetType.Other)
                    {
                        var other = (from a in _dbCommonMethods.VIEW_FRONTEND_M_ASSET_OTHER
                                     join b in _dbCommonMethods.VIEW_FRONTEND_M_BRAND on a.BRAND_ID equals b.BRAND_ID into g1
                                     from b in g1.DefaultIfEmpty()

                                     join c in _dbCommonMethods.VIEW_FRONTEND_M_MODEL on a.MODEL_ID equals c.MODEL_ID into g2
                                     from c in g2.DefaultIfEmpty()

                                     where a.ASSET_ID == AssetID
                                     select new { a, b, c }).FirstOrDefault();

                        if (other != null)
                        {
                            toReturn += "หมายเลขสินค้า = '" + other.a.PRODUCT_NO + "',<br/>";
                            toReturn += "ยี่ห้อสินค้า = '" + other.b?.NAME_THA + "',<br/>";
                            toReturn += "รุ่นสินค้า = '" + other.c?.NAME_THA + "'";
                        }
                    }
                }
                return toReturn;
            }
            catch(Exception)
            {
                throw;
            }

        }

        public static string stringDateTimeNullable(DateTime? data)
        {
            string toReturn = "";
            toReturn = (data != null) ? data.Value.ToString("dd/MM/yyyy") : "";

            return toReturn;
        }


    }
}