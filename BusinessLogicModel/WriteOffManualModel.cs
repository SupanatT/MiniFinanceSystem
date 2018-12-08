using MiniFinance.Common;
using MiniFinance.Models.DataModel;
using MiniFinance.ViewModel.WriteOffManual;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MiniFinance.Models.BusinessLogicModel
{
    public class WriteOffManualModel : BaseBLLModel
    {
        public bool CallWriteOffApi(MainDataViewModel viewModel, LoginModel.Login login)
        {
            try
            {
                viewModel.Username = login.username;
                viewModel.CompanyCode = login.compCode;
                viewModel.BranchCode = login.branchCode;

                string apiURL = Constants.AppSetting.PathCashReceiveAPI + Constants.CashReceiveAPIMethod.UpdateWriteOff;
                HttpClient client = new HttpClient();
                var dataSend = JsonConvert.SerializeObject(viewModel);
                var stringContent = new StringContent(dataSend, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiURL, stringContent).Result;
                var readAPI = response.Content.ReadAsStringAsync().Result;
                bool result = JsonConvert.DeserializeObject<bool>(readAPI);

                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public void BindData(MainDataViewModel viewModel)
        {
            try
            {
                decimal vatRate = CommonMethods.GetVatRate();
                var obj = (from con in _dbContext.T_CONTRACT
                           join ins in _dbContext.VIEW_CASH_RECEIVE_T_INSTALLMENT on con.CONT_CODE equals ins.Ins_ContractCode
                           join cus in _dbContext.VIEW_M_CUST_CUSTOMER on con.CUSTOMER_CODE equals cus.CUSTOMER_CODE into g1
                           from cus in g1.DefaultIfEmpty()

                           where con.CONT_CODE == viewModel.ContractCode
                           select new { con, ins, cus }
                           ).FirstOrDefault();

                if (obj != null)
                {
                    viewModel.LoanTypeCode = obj.con.LOAN_TYPE_ID;
                    viewModel.LoanType = CommonMethods.GetMasterDescription(obj.con.LOAN_TYPE_ID, Constants.MasterType.LoanType);
                    viewModel.LoanProductType = CommonMethods.GetMasterDescription(obj.con.LOAN_PRODUCT_ID, Constants.MasterType.ProductType);
                    viewModel.CustomerIDCard = obj.cus?.ID_CARD_NO;
                    viewModel.CustomerName = CommonMethods.GetCustomerFullName(obj.con.CUSTOMER_CODE);

                    var insdObj = _dbContext.VIEW_CASH_RECEIVE_T_INSTALLMENT_DETAIL.FirstOrDefault(x => x.InsD_ContractCode == viewModel.ContractCode);
                    viewModel.Installment = insdObj?.InsD_InstallmentTotal;
                    viewModel.InstallmentVat = insdObj?.InsD_Vat;

                    viewModel.OutstandingBalance = obj.ins.Ins_InstallmentTotal - obj.ins.Ins_InstallmentReceived.GetValueOrDefault();

                    var suspense = _dbContext.VIEW_CASH_RECEIVE_T_STATEMENT.Where(x => x.STM_ContractCode == viewModel.ContractCode && x.STM_Status == Constants.ContractStatus.Active);
                    if (suspense.Count() > 0)
                    {
                        viewModel.SuspenseAmount = suspense.Sum(x => x.STM_Amount - x.STM_PayAmount);
                    }
                    else
                    {
                        viewModel.SuspenseAmount = 0;
                    }

                    var OverdueObj = _dbContext.VIEW_CASH_RECEIVE_T_INSTALLMENT_DETAIL.Where(x => x.InsD_ContractCode == viewModel.ContractCode
                                    && x.InsD_FlagMatch != true && x.InsD_DueDate < DateTimeNow);
                    if (OverdueObj.Count() > 0)
                    {
                        viewModel.OverdueAmount = OverdueObj.Sum(x => x.InsD_InstallmentTotal - x.InsD_InterestReceived).GetValueOrDefault();
                        viewModel.OverdueVat = OverdueObj.Sum(x => (x.InsD_Vat ?? 0) - (x.InsD_VatReceived ?? 0));
                    }
                    else
                    {
                        viewModel.OverdueAmount = 0;
                        viewModel.OverdueVat = 0;
                    }


                    viewModel.PenaltyAmount = obj.ins.Ins_PenaltyTotalAmount.GetValueOrDefault() - obj.ins.Ins_PenaltyTotalReceived.GetValueOrDefault();
                    viewModel.ContractStatus = CommonMethods.GetMasterDescription(obj.con.STATUS, Constants.MasterType.ContractStatus);


                    var CollectorObj = _dbContext.S_CONT_COLLECTOR.FirstOrDefault(x => x.CONTRACT_CODE == viewModel.ContractCode);

                    if (CollectorObj != null)
                    {
                        viewModel.FollowUpBy = CollectorObj.TITLE_NAME_TH + " " + CollectorObj.FIRST_NAME_TH + " " + CollectorObj.LAST_NAME_TH;
                    }


                    viewModel.LastReceiptDate = obj.ins.Ins_LastPaidDate;
                    viewModel.TransactionDate = DateTimeNow;

                    viewModel.LetterFee = _dbContext.S_COLLECTION_FEE.Where(x => x.FEE_CONTRACTNO == viewModel.ContractCode && x.FEE_STATUS == "A").Sum(x => x.FEE_RUNNINGLETTERFEE).GetValueOrDefault();
                    viewModel.FollowUpFee = _dbContext.S_FOLLOWUP_NOTE.Where(x => x.NOTE_CONTRACTNO == viewModel.ContractCode && x.NOTE_STATUS == "A").Sum(x => x.NOTE_FEE).GetValueOrDefault();

                    if (obj.con.LOAN_TYPE_ID == Constants.LoanType.LN)
                    {
                        viewModel.PrincipalOutstanding = obj.ins.Ins_Principal.GetValueOrDefault() - obj.ins.Ins_PrincipalReceived.GetValueOrDefault();
                        viewModel.InterestOutstanding = obj.ins.Ins_Interest.GetValueOrDefault() - obj.ins.Ins_InterestReceived.GetValueOrDefault();

                        viewModel.lsFee = new List<FieldTableFee>();
                        viewModel.lsDeduct = new List<FieldTableFee>();


                        //=========================================

                        var chkInReceipt = (from recH in _dbContext.VIEW_CASH_RECEIVE_T_RECEIPT
                                            join recD in _dbContext.VIEW_CASH_RECEIVE_T_RECEIPT_DETAIL on recH.Receipt_No equals recD.RCD_ReceiptNo
                                            where recH.Receipt_ContractCode == viewModel.ContractCode
                                            select new { recD }
                                            );

                        List<string> receiptTransCode = new List<string>(); //convert transcode to string
                        foreach (var recData in chkInReceipt)
                        {
                            receiptTransCode.Add(recData.recD.RCD_TransCode.ToStringOrEmpty());
                        }

                        var FeeObj = _dbContext.VIEW_CASH_RECEIVE_T_FEE.Where(x => x.Fee_ContractCode == viewModel.ContractCode)
                                    .GroupBy(g => new { g.Fee_Code })
                                    .Select(x => new { FeeCode = x.Key.Fee_Code, FeeAmountOutstanding = x.Sum(y => y.Fee_Amount - y.Fee_Received) });

                        foreach (var foj in FeeObj)
                        {
                            if (foj.FeeAmountOutstanding > 0)
                            {
                                viewModel.lsFee.Add(new FieldTableFee
                                {
                                    TransDesc = CommonMethods.GetTransCodeDescription(foj.FeeCode.ToStringOrEmpty()),
                                    Amount = foj.FeeAmountOutstanding.GetValueOrDefault()
                                });
                            }

                        }

                        var DeductObj = _dbContext.VIEW_FRONTEND_R_CONT_FEE_ONETIME.Where(x => x.CONT_CODE == viewModel.ContractCode
                                        && x.FEE_CAL_EIRR_FLAG == true
                                        && !(receiptTransCode.Contains(x.FEE_CODE)));

                        foreach (var doj in DeductObj)
                        {
                            viewModel.lsDeduct.Add(new FieldTableFee
                            {
                                TransDesc = CommonMethods.GetTransCodeDescription(doj.FEE_CODE),
                                Amount = doj.FEE_BAHT.GetValueOrDefault()
                            });
                        }

                        //====================================================================

                        viewModel.WriteOffAmount = viewModel.OutstandingBalance.GetValueOrDefault();
                        viewModel.WriteOffVatAmount = 0;

                    }
                    else if (obj.con.LOAN_TYPE_ID == Constants.LoanType.HP)
                    {
                        viewModel.OutstandingVat = obj.ins.Ins_Vat.GetValueOrDefault() - obj.ins.Ins_VatReceived.GetValueOrDefault();

                        viewModel.WriteOffAmount = viewModel.OutstandingBalance.GetValueOrDefault() - viewModel.OutstandingVat.GetValueOrDefault() - viewModel.UnrealizedIncome.GetValueOrDefault();

                        if (viewModel.WriteOffAmount > 0)
                        {
                            viewModel.WriteOffVatAmount = viewModel.WriteOffAmount * 7 / 107;
                        }
                    }
                    viewModel.WriteOffExVatAmount = viewModel.WriteOffAmount - viewModel.WriteOffVatAmount;

                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void Save(ACCOUNTINGEntities db, MainDataViewModel viewModel, LoginModel.Login login)
        {
            var removeObj = db.T_WRITE_OFF.Where(x => x.CONTRACT_CODE == viewModel.ContractCode);
            if (removeObj.Count() > 0)
            {
                db.T_WRITE_OFF.RemoveRange(removeObj);
            }

            db.T_WRITE_OFF.Add(new T_WRITE_OFF
            {
                CONTRACT_CODE = viewModel.ContractCode,
                WRITEOFF_EXVAT_AMOUNT = viewModel.WriteOffExVatAmount,
                WRITEOFF_VAT_AMOUNT = viewModel.WriteOffVatAmount,
                WRITEOFF_AMOUNT = viewModel.WriteOffAmount,
                WRITEOFF_DATE = viewModel.WriteOffDate,
                WRITEOFF_STATUS = Constants.WriteOffStatus.WritOffFinish,
                CREATE_BY = login.username,
                CREATE_DATE = DateTimeNow,
                COMP_CODE = login.compCode,
                BRANCH_CODE = login.branchCode
            });

        }

    }
}