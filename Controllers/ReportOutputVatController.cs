using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MiniFinance.Common;
using MiniFinance.ViewModel.ReportOutputVat;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Controllers
{
    public class ReportOutputVatController : BaseController
    {
        // GET: ReportOutputVat
        [SessionTimeout]
        public ActionResult Index()
        {
            SearchViewModel viewModel = new SearchViewModel();
            return View(viewModel);
        }

        public ActionResult Search(SearchViewModel viewModel)
        {
            List<tbDetailViewModel> result = new List<tbDetailViewModel>();
            try
            {
                result = ResultQuery(viewModel);

                return Json(new
                {
                    r = Constants.Result.True,
                    msg = "",
                    result = PartialView(this, "_PartialViewTable", result)
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        private List<tbDetailViewModel> ResultQuery(SearchViewModel viewModel)
        {
            List<tbDetailViewModel> result = new List<tbDetailViewModel>();
            try
            {
                var obj = db.SP_REPORT_OUTPUT_VAT(viewModel.BranchCode, viewModel.FromInvoiceDate, viewModel.ToInvoiceDate);


                foreach (var item in obj)
                {
                    result.Add(new tbDetailViewModel
                    {
                        CompBranch = item.BRANCH_NAME,
                        InvoiceDate = item.INVOICE_DATE,
                        InvoiceNo = item.INVOICE_NO,
                        ContractCode = item.CONTRACT_CODE,
                        CustomerName = item.CUSTOMER_NAME,
                        CustomerTaxNo = item.CUSTOMER_TAX_NO,
                        CustomerBranchCode = item.CUSTOMER_BRANCH,
                        Period = item.PERIOD,
                        TransCode = item.TRANS_CODE,
                        TransDesc = item.TRANS_DESC,
                        AmountExVat = item.AMOUNT_EX_VAT,
                        VatAmount = item.VAT_AMOUNT
                    });
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Print(SearchViewModel viewModel)
        {
            ReportDocument rd = new ReportDocument();

            try
            {

                
                var dtDetail = new DsReports.R_ReportOutputVatDetailDataTable();
                var dtHead = new DsReports.R_ReportOutputVatHeadDataTable();

                var obj = db.SP_REPORT_OUTPUT_VAT(viewModel.BranchCode, viewModel.FromInvoiceDate, viewModel.ToInvoiceDate);

                

                foreach (var itemD in obj)
                {
                    dtDetail.AddR_ReportOutputVatDetailRow(
                       BranchName: itemD.BRANCH_NAME,
                       CompanyName: itemD.COMP_NAME,
                       BranchAddress: itemD.BRANCH_ADDRESS,
                       CompanyTaxNo: itemD.COMP_TAX_NO,
                       InvoiceDate: itemD.INVOICE_DATE != null ? itemD.INVOICE_DATE.ToString("dd/MM/yyyy") : "",
                       InvoiceNo: itemD.INVOICE_NO,    
                       ContractCode: itemD.CONTRACT_CODE,
                       CustomerName: itemD.CUSTOMER_NAME,
                       CustomerTaxNo: itemD.CUSTOMER_TAX_NO,
                       CustomerBranch: "",
                       Period: itemD.PERIOD.ToStringOrEmpty(),
                       TransDesc: itemD.TRANS_DESC,
                       AmountExVat: itemD.AMOUNT_EX_VAT,
                       VatAmount: itemD.VAT_AMOUNT

                     );
                }

                string FromInvoiceDateShow = "";
                string ToInvoiceDateShow = "";
                if(viewModel.FromInvoiceDate != null)
                {
                    FromInvoiceDateShow = viewModel.FromInvoiceDate.Value.Day + " " + CommonMethods.GetMonthText(viewModel.FromInvoiceDate.Value.Month) + " " + viewModel.FromInvoiceDate.Value.ToString("yyyy");
                }
                if (viewModel.ToInvoiceDate != null)
                {
                    ToInvoiceDateShow = viewModel.ToInvoiceDate.Value.Day + " " + CommonMethods.GetMonthText(viewModel.ToInvoiceDate.Value.Month) + " " + viewModel.ToInvoiceDate.Value.ToString("yyyy");
                }

                dtHead.AddR_ReportOutputVatHeadRow(
                    FromInvoiceDate: FromInvoiceDateShow,
                    ToInvoiceDate: ToInvoiceDateShow
                    );

                rd.Load(Path.Combine(Server.MapPath("~/Reports/OutputVat/ReportOutputVat.rpt")));
                rd.SummaryInfo.ReportTitle = "";
                rd.SummaryInfo.ReportAuthor = "";
                rd.SummaryInfo.ReportComments = "";


                
                rd.Database.Tables[0].SetDataSource((DataTable)dtDetail);
                rd.Database.Tables[1].SetDataSource((DataTable)dtHead);



                Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}