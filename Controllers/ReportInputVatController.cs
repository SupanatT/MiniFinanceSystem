using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MiniFinance.Common;
using MiniFinance.ViewModel.ReportInputVat;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Controllers
{
    public class ReportInputVatController : BaseController
    {
        // GET: ReportInputVat
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

        [HttpPost]
        public ActionResult Print(SearchViewModel viewModel)
        {
            ReportDocument rd = new ReportDocument();
            List<string> LsMergePath = new List<string>();
            SortedSet<string> reportBetweenMonth = new SortedSet<string>();
            int countFile = 0;

            List<tbDetailViewModel> detailData = new List<tbDetailViewModel>();
            
            try
            {
                detailData = ResultQuery(viewModel);

                foreach(var element in detailData) 
                {
                    if(element.ClaimDate != null)
                    {
                        reportBetweenMonth.Add(element.ClaimDate.Value.ToString("yyyyMM"));
                    }
                }
               
                

                foreach (var MonthYearReport in reportBetweenMonth) //report ประจำแต่ละเดือน
                {
                    var dtHead = new DsReports.R_ReportInputVatDataTable();
                    var dtDetail = new DsReports.R_ReportInputVatDetailDataTable();

                    countFile++;
                    string mapPath = Server.MapPath(Constants.MapPath.TempPdf);
                    string fileName = DateTime.Now.ToString("ddMMyyyy_hhmmss") + "_" + countFile.ToString() + ".pdf";
                    var mapPathFileName = mapPath + fileName;

                    int i = 0;
                    decimal sumExVatAmount = 0;
                    decimal sumVatAmount = 0;
                    var detailDataInMonthYear = detailData.Where(x => (x.ClaimDate.HasValue ? x.ClaimDate.Value.ToString("yyyyMM") == MonthYearReport : false));
                    foreach (var item in detailDataInMonthYear)
                    {
                        i++;
                        sumExVatAmount += item.AmountExVat;
                        sumVatAmount += item.VatAmount;

                        dtDetail.AddR_ReportInputVatDetailRow(
                            OrderNum: i.ToStringOrEmpty(),
                            ClaimDate: item.ClaimDate != null ? item.ClaimDate.Value.ToString(Constants.DateFormat.DDMMYYYY) : "",
                            InvoiceNo: item.InvoiceNo,
                            ContractCode: item.ContractCode,
                            SellerName: item.SellerName,
                            ExVatAmount: String.Format("{0:n}", item.AmountExVat),
                            VatAmount: String.Format("{0:n}", item.VatAmount)
                        );
                    }


                    var CompObj = db.VIEW_M_COMPANY_BRANCH.FirstOrDefault(x => x.COMPANY_BRANCH_CODE == SessionLoginInfo.login.branchCode && x.LANGUAGE_TYPE == "THA");

                    if (CompObj != null)
                    {
                        string AddrLine1 = CompObj.ADDRESS_HOME + " " + CompObj.ROAD + " " + CompObj.SUBDISTRICT;
                        string AddrLine2 = CompObj.DISTRICT + " " + CompObj.PROVINCE + " " + CompObj.ZIPCODE_ID;

                        int ReportMonth = (MonthYearReport.Length >= 2) ? MonthYearReport.Substring(MonthYearReport.Length - 2).ToInt() : 1;
                        string ReportYear = (MonthYearReport.Length >= 4) ? MonthYearReport.Substring(0, 4) : DateTime.Now.ToString("yyyy");

                        dtHead.AddR_ReportInputVatRow(
                            BranchName: CompObj.COMPANY_BRANCH_NAME,
                            CompanyName: CompObj.NAME,
                            AddressLine1: AddrLine1,
                            AddressLine2: AddrLine2,
                            CompTaxNo: CompObj.TAX_NO,
                            AsOfMonthYear: (viewModel.FromMonthYear != null) ? CommonMethods.GetMonthText(ReportMonth) + " " + ReportYear : "",
                            SumExVatAmount: String.Format("{0:n}", sumExVatAmount),
                            SumVatAmount: String.Format("{0:n}", sumVatAmount)
                        );
                    }

                    //////////////////////////////////////////////////////////////////////
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/InputVat/ReportInputVat.rpt")));

                    rd.Database.Tables[0].SetDataSource((DataTable)dtDetail);
                    rd.Database.Tables[1].SetDataSource((DataTable)dtHead);

                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, mapPathFileName);
                    LsMergePath.Add(mapPathFileName);

                }


                string exportFileName = DateTime.Today.ToString("ddMMyyyy_hhmmss") + ".pdf";
                var fileBytes = MergePdf(LsMergePath, exportFileName);

                MemoryStream ms = new MemoryStream(fileBytes);
                return File(ms, "application/pdf");

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
                var obj = (from a in db.T_INPUT_VAT
                           where a.ACTIVE == true
                           && (a.CONT_CODE.CompareTo(viewModel.FromContractCode) >= 0 || viewModel.FromContractCode == null) //CompareTo use for where between string
                           && (a.CONT_CODE.CompareTo(viewModel.ToContractCode) <= 0 || viewModel.ToContractCode == null)
                           && (a.BRANCH_CODE == viewModel.BranchCode || viewModel.BranchCode == null)
                           && ((a.CLAIM_TAX_DATE.HasValue ? a.CLAIM_TAX_DATE >= viewModel.FromMonthYear : false) || viewModel.FromMonthYear == null)
                           && ((a.CLAIM_TAX_DATE.HasValue ? a.CLAIM_TAX_DATE <= viewModel.ToMonthYear : false) || viewModel.ToMonthYear == null)
                           orderby a.CLAIM_TAX_DATE, a.INVOICE_NO
                           select new { a }
                       );


                foreach (var item in obj)
                {
                    result.Add(new tbDetailViewModel
                    {
                        ClaimDate = item.a.CLAIM_TAX_DATE,
                        InvoiceNo = item.a.INVOICE_NO,
                        ContractCode = item.a.CONT_CODE,
                        SellerName = item.a.PAYEE_NAME,
                        AmountExVat = item.a.EX_VAT_AMOUNT.GetValueOrDefault(),
                        VatAmount = item.a.VAT_AMOUNT.GetValueOrDefault()
                    });
                }

                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}