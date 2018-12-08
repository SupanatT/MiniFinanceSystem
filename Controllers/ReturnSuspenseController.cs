using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MiniFinance.Common;
using MiniFinance.Models.BusinessLogicModel;
using MiniFinance.Models.DataModel;
using MiniFinance.ViewModel.ReturnSuspense;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Controllers
{
    public class ReturnSuspenseController : BaseController
    {
        ReturnSuspenseModel bllModel = new ReturnSuspenseModel();

        [SessionTimeout]
        public ViewResult Index()
        {

            SearchViewModel viewModel = new SearchViewModel();
            return View(viewModel);
        }

        public JsonResult Search(SearchViewModel viewModel)
        {
            try
            {
                var objResult = db.SP_GET_TRANSACTION_SUSPENSE(viewModel.ContractCode, viewModel.CustomerName,
                                viewModel.FromStmDate, viewModel.ToStmDate, viewModel.FromReconcileDate, viewModel.ToReconcileDate, viewModel.LoanType,
                                SessionLoginInfo.login.compCode, SessionLoginInfo.login.branchCode).ToList();

                return Json(new
                {
                    r = Constants.Result.True,
                    msg = "",
                    result = PartialView(this, "_PartialViewTable", objResult)
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult Save(SaveViewModel viewModel)
        {
            bool isSuccess = false;
            bool isFinalSuccess = false;
            string msg = Constants.Message.SaveUnsuccess;
            viewModel.SystemCode = "ACCOUNTING";
            viewModel.CompCode = SessionLoginInfo.login.compCode;
            viewModel.BranchCode = SessionLoginInfo.login.branchCode;
            viewModel.Username = SessionLoginInfo.login.username;
            
            try
            {
                viewModel.lsModel = viewModel.lsModel.Where(x => x.isChecked).ToList();
                            
                bool IsPaymentApiSuccess = bllModel.CallAPI(viewModel, Constants.AppSetting.PathPaymentAPI + Constants.PaymentAPIMethod.ReturnSuspense);
                bool IsCRApiSuccess = bllModel.CallAPI(viewModel, Constants.AppSetting.PathCashReceiveAPI + Constants.CashReceiveAPIMethod.ReturnSuspense);

                if(IsPaymentApiSuccess && IsCRApiSuccess)
                {
                    foreach(var data in viewModel.lsModel)
                    {
                        db.T_SUSPENSE_ACTION.Add(new T_SUSPENSE_ACTION
                        {
                            CR_STM_ID = data.STM_Id,
                            SUSPENSE_ACTION_TYPE = Constants.SuspenseActionType.PayReturn,
                           // PAYMENT_CODE = "",
                            SUSPENSE_AMOUNT = data.SuspenseAmt,
                            CREATE_DATE = DateTimeNow,
                            CREATE_BY = SessionLoginInfo.login.username,
                            COMP_CODE = SessionLoginInfo.login.compCode,
                            BRANCH_CODE = SessionLoginInfo.login.branchCode
                        });
                    }

                    db.SaveChanges();
                    isFinalSuccess = true;
                }

                if (isFinalSuccess)
                {
                    msg = Constants.Message.SaveSuccess;
                    isSuccess = true;
                }

                return Json(new
                {
                    result = isSuccess,
                    msg = msg
                });
            }
            catch(Exception)
            {
                throw;
            }
            
            
        }

    }
}