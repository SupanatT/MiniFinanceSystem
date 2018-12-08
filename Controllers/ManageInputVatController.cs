using MiniFinance.Common;
using MiniFinance.Models.BusinessLogicModel;
using MiniFinance.ViewModel.ManageInputVat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Controllers
{
    public class ManageInputVatController : BaseController
    {
        ManageInputVatModel bllModel = new ManageInputVatModel();
        // GET: ManageInputVat
        [SessionTimeout]
        public ActionResult Index()
        {
            SearchViewModel viewModel = new SearchViewModel();
            return View(viewModel);
        }


        public ActionResult ManagePanel(SearchViewModel viewModel)
        {
            try
            {
                viewModel.manageVM.ContractCode = viewModel.ContractCode;

                return PartialView("Manage", viewModel.manageVM);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public ActionResult Search(SearchViewModel viewModel)
        {
            viewModel.manageVM = new ManageViewModel();
            viewModel.manageVM.lsDetail = new List<ManageViewModel>();

            try
            {
                decimal vatRate = CommonMethods.GetVatRate();
                var tCon = db.T_CONTRACT.FirstOrDefault(x => x.CONT_CODE == viewModel.ContractCode);
                if (tCon != null)
                {
                    viewModel.CustIDCard = db.VIEW_M_CUST_CUSTOMER.FirstOrDefault(x => x.CUSTOMER_CODE == tCon.CUSTOMER_CODE)?.ID_CARD_NO;
                    viewModel.CustName = CommonMethods.GetCustomerFullName(tCon.CUSTOMER_CODE);

                    if (tCon.INPUT_VAT_FLAG == true)
                    {
                        bllModel.GetNewData(viewModel);
                    }
                    else
                    {
                        bllModel.GetOldData(viewModel);
                    }
                }

                return Json(new
                {
                    r = Constants.Result.True,
                    //msg = "ไม่พบเลขที่สัญญาในระบบ"
                    result = PartialView(this, "Index", viewModel)
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }

            
        }

        [HttpPost]
        public ActionResult ModalSearchSelect(string code)
        {
            var model = new SearchViewModel();
            model.ContractCode = code;

            return RedirectToAction("Search", model);
        }

        public ActionResult _tbDetails(ManageViewModel viewModel)
        {
            try
            {
                return PartialView(viewModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AddTransList(ManageViewModel viewModel)
        {
            try
            {
                viewModel.setupAssetDescription();

                ManageViewModel AddedObj = new ManageViewModel();
                viewModel.AddOrEditRowObj(ref AddedObj);
                viewModel.lsDetail.Add(AddedObj);

                viewModel.ClearModel();

                return PartialView("Manage", viewModel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult DefaultDataEditRow(ManageViewModel viewModel, int rowIndex)
        {
            try
            {
                viewModel.setupAssetDescription();

                var obj = viewModel.lsDetail.FirstOrDefault(x => x.rowIndex == rowIndex);
                if(obj != null)
                {
                    viewModel.mode = "Edit";

                    viewModel.PayHeaderID = obj.PayHeaderID;
                    viewModel.TransCode = obj.TransCode;
                    viewModel.TransDesc = obj.TransDesc;
  
                    viewModel.ReceiveDocDate = obj.ReceiveDocDate;
                    viewModel.InvoiceDate = obj.InvoiceDate;
                    viewModel.ClaimDate = obj.ClaimDate;
                    viewModel.InvoiceNo = obj.InvoiceNo;
                    viewModel.ExVatAmount = obj.ExVatAmount;
                    viewModel.VatAmount = obj.VatAmount;
                    viewModel.TotalAmount = obj.TotalAmount;
                    viewModel.FlagReceiveInvoice = obj.FlagReceiveInvoice;
                    viewModel.FlagReceiveReceipt = obj.FlagReceiveReceipt;
                    viewModel.PayeeCode = obj.PayeeCode;
                    viewModel.PayeeName = obj.PayeeName;
                    viewModel.SellerCode = obj.SellerCode;
                    viewModel.SellerName = obj.SellerName;
                    viewModel.AssetID = obj.AssetID;
                    viewModel.AssetType = obj.AssetType;
                }


                return PartialView("Manage", viewModel);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(ManageViewModel viewModel, int rowIndex)
        {
            try
            {
                viewModel.setupAssetDescription();

                var editObj = viewModel.lsDetail.FirstOrDefault(x => x.rowIndex == rowIndex);
                if(editObj != null)
                {
                    viewModel.AddOrEditRowObj(ref editObj);
                    viewModel.ClearModel();
                }

                return PartialView("Manage", viewModel);
            }
            catch(Exception e)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult Save(ManageViewModel viewModel)
        {
            bool result = false;
            string message = "";
            try
            {
                foreach(var item in viewModel.lsDetail)
                {
                    if(item.ReceiveDocDate == null || item.InvoiceDate == null 
                        || item.ClaimDate == null || item.InvoiceNo == null)
                    {
                        message = "กรุณาระบุข้อมูลวันที่รับเอกสาร, วันที่บนใบกำกับ, วันที่เคลมภาษี และเลขที่ใบกำกับ ให้ครบทุกรายการ";
                        return Json(new { result = result, msg = message }, JsonRequestBehavior.AllowGet);
                    }


                    bllModel.Save(db, viewModel.ContractCode, item, SessionLoginInfo.login);
                }

                db.SaveChanges();
                result = true;
                message = Constants.Message.SaveSuccess;


                return Json(new { result = result, msg = message }, JsonRequestBehavior.AllowGet); 
            }
            catch(Exception)
            {
                message = Constants.Message.SaveUnsuccess;
                return Json(new { result = false, msg = message }, JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}