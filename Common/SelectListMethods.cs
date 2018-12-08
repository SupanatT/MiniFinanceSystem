using MiniFinance.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Common
{
    public static class SelectListMethods
    {
        private static readonly ACCOUNTINGEntities _dbContext = new ACCOUNTINGEntities();

        public static IEnumerable<SelectListItem> GetMasterForDropdown(string masterType)
        {
            return _dbContext.VIEW_M_MASTER.Where(x => x.MASTER_TYPE_ID == masterType && x.ACTIVE).Select(x => new SelectListItem { Value = x.MASTER_ID, Text = x.NAME_THA }).ToList();
        }
        public static IEnumerable<SelectListItem> GetTransCodeForDropdown()
        {
            var returnVal = _dbContext.VIEW_M_TRANS_CODE.Where(x => x.ACTIVE).Select(x => new SelectListItem { Value = x.TRANS_CODE.ToString(), Text = x.TRANS_CODE.ToString() + ":" + x.DESCRIPTION_THA }).ToList();
            return returnVal.Where(x => x.Value.ToInt() < 10000).OrderBy(x => x.Value.ToInt());
        }
        public static IEnumerable<SelectListItem> GetCompanyBranchForDropdown()
        {
            var returnVal = _dbContext.VIEW_M_COMPANY_BRANCH.Where(x => x.LANGUAGE_TYPE == "THA" && x.ACTIVE == true).Select(x => new SelectListItem { Value = x.COMPANY_BRANCH_CODE, Text = x.COMPANY_BRANCH_NAME }).ToList();
            return returnVal;
        }
        public static IEnumerable<SelectListItem> GetContractForDropdown()
        {
            var returnVal = _dbContext.T_CONTRACT.Select(x => new SelectListItem { Value = x.CONT_CODE, Text = x.CONT_CODE }).ToList();
            return returnVal;
        }
        public static IEnumerable<SelectListItem> GetPrintScopeSBTForDropdown()
        {
            var obj = new List<SelectListItem>();
            obj.Add(new SelectListItem
            {
                Text = "รายการรับชำระ (ดอกเบี้ย+ค่าธรรมเนียมเปิดวงเงิน)",
                Value = "1",
            });
            obj.Add(new SelectListItem
            {
                Text = "ค่าธรรมเนียมเปิดวงเงิน 1% ของเงินต้น-สัญญาใหม่",
                Value = "2",
            });


            return obj;
        }
        public static IEnumerable<SelectListItem> GetPastMonthYearForDropdown(int numMonth, string StartOrEndDay = "S") //numMonth ย้อนหลังกี่เดือน
        {
            DateTime today = DateTime.Today;
            var obj = new List<SelectListItem>();

            for (int i = 0; i < numMonth; i++)
            {
                var SubtractMonth = today.AddMonths(i * -1);
                DateTime newDt = new DateTime();

                if (StartOrEndDay == "S") //วันที่ 1nthYearForDropdown
                {
                    newDt = new DateTime(SubtractMonth.Year, SubtractMonth.Month, 1);
                }
                else if (StartOrEndDay == "E") //วันสุดท้ายของเดือน
                {
                    newDt = new DateTime(SubtractMonth.Year, SubtractMonth.Month, DateTime.DaysInMonth(SubtractMonth.Year, SubtractMonth.Month));
                }

                obj.Add(new SelectListItem
                {
                    Text = CommonMethods.GetMonthText(SubtractMonth.Month) + " " + SubtractMonth.ToString("yyyy"),
                    Value = newDt.ToString(Constants.DateFormat.DDMMYYYY)
                });
            }

            return obj;
        }

        public static IEnumerable<SelectListItem> GetContractInBranchForDropdown(string CompCode, string BranchCode)
        {
            var returnVal = _dbContext.T_CONTRACT.Where(x => x.COMP_CODE == CompCode && x.COMP_BRANCH_CODE == BranchCode).Select(x => new SelectListItem { Value = x.CONT_CODE, Text = x.CONT_CODE }).ToList();
            return returnVal;
        }

        public static IEnumerable<SelectListItem> GetContractPaymentVatForDropdown()
        {
            var obj = (from a in _dbContext.T_CONT_PAYMENT_HEADER
                       join b in _dbContext.T_CONT_PAYMENT_DETAIL on a.HEADER_ID equals b.HEADER_ID
                       where a.ACTIVE == true
                       && b.TRANS_TYPE == Constants.PaymentTranType.Payment
                       && (b.TRANS_SUB_TYPE == Constants.PaymentTranSubType.Commission || b.TRANS_SUB_TYPE == Constants.PaymentTranSubType.ProductCost)
                       select new { a, b }
                       ).GroupBy(g => new {
                           g.a.CONT_CODE,
                       })
                        .Select(x => new SelectListItem { Value = x.Key.CONT_CODE, Text = x.Key.CONT_CODE }).ToList();

            return obj;
        }
        public static IEnumerable<SelectListItem> CustomerDropdown()
        {
            var returnVal = _dbContext.VIEW_CASH_RECEIVE_T_STATEMENT;
            var returnVal2 = returnVal.Select(x => new SelectListItem { Value = x.STM_CustomerName, Text = x.STM_CustomerName }).ToList();
            return returnVal2;
        }
        public static IEnumerable<SelectListItem> ContractStatusItem()
        {
            var returnVal = _dbContext.VIEW_M_MASTER.Where(x => x.MASTER_TYPE_ID == "ContractSubStatus").Select(x => new SelectListItem { Value = x.MASTER_ID, Text = x.NAME_THA }).ToList();
            return returnVal;

        }

        public static IEnumerable<SelectListItem> PaymentChannelItem()
        {
            var returnVal = _dbContext.VIEW_M_MASTER.Where(x => x.MASTER_TYPE_ID == "PaymentChannel").Select(x => new SelectListItem { Value = x.MASTER_ID, Text = x.NAME_THA }).ToList();
            return returnVal;

        }
        public static IEnumerable<SelectListItem> GetBankForDropdown()
        {
            var returnVal = _dbContext.VIEW_M_BANK.Where(x => x.ACTIVE).Select(x => new SelectListItem { Value = x.BANK_ID, Text = x.BANK_ID.ToString() + ":" + x.NAME_THA }).ToList();
            return returnVal;
        }
        public static IEnumerable<SelectListItem> GetBankBranchForDropdown(string bankCode)
        {
            var returnVal = _dbContext.VIEW_M_BANK_BRANCH.Where(x => x.ACTIVE && x.BANK_ID == bankCode).Select(x => new SelectListItem { Value = x.BANKBRANCH_ID, Text = x.BANKBRANCH_ID.ToString() + ":" + x.NAME_THA }).ToList();
            return returnVal;
        }
        public static IEnumerable<SelectListItem> InputVatTransTypeForDropdown(string ContractCode)
        {
            var obj = new List<SelectListItem>();

            var query = (from a in _dbContext.VIEW_FRONTEND_R_CONT_ASSET
                         join b in _dbContext.VIEW_FRONTEND_R_CONTRACT on a.CONTRACT_ID equals b.CONTRACT_ID
                         where b.CONT_CODE == ContractCode && b.LOAN_TYPE_ID == Constants.LoanType.HP && a.ACTIVE == true
                         select new { a });
            int index = 0;
            bool haveIndex = false;
            if (query.Where(x => x.a.ASSET_TYPE_ID == Constants.AssetType.Other).Count() >= 2)
            {
                haveIndex = true;
            }

            foreach (var item in query)
            {
                if (item.a.ASSET_TYPE_ID == Constants.AssetType.Other && haveIndex)
                {
                    index++;
                }

                obj.Add(new SelectListItem
                {
                    Text = CommonMethods.GetFormatTextProductTrans(item.a.ASSET_TYPE_ID, haveIndex, index.ToString()),
                    Value = Constants.TransCode.ProductHirePurchase,
                });
            }

            return obj;
        }
        public static IEnumerable<SelectListItem> AsOfDateMonthYear(DateTime date)
        {
            List<SelectListItem> lsSli = new List<SelectListItem>();
            try
            {
                ;
                DateTime dateNow = date;
                for (int i = 0; i < 12; i++)
                {
                    var valDate = dateNow.AddMonths(-i).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"));
                    var txtDate = CommonMethods.GetMonthText(dateNow.AddMonths(-i).Month) + " " + dateNow.AddMonths(-i).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"));
                    SelectListItem sli = new SelectListItem()
                    {
                        Value = valDate,
                        Text = txtDate
                    };

                    lsSli.Add(sli);
                }
                return lsSli;
            }
            catch
            (Exception)
            {
                throw;
            }
        }

    }
}