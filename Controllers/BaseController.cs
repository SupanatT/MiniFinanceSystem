using MiniFinance.Common;
using MiniFinance.Models;
using MiniFinance.Models.DataModel;
using MiniFinance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MiniFinance.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ACCOUNTINGEntities db = new ACCOUNTINGEntities();
        public LoginModel SessionLoginInfo
        {
            get
            {
                if (Session[Constants.SESSION_KEY.LoginInfo].IsNull())
                    return new LoginModel();

                return (LoginModel)Session[Constants.SESSION_KEY.LoginInfo];
            }
            set
            {
                Session[Constants.SESSION_KEY.LoginInfo] = value;
            }
        }

        public DateTime DateTimeNow
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(CommonMethods.GetSysConfigValue(Constants.SysConfigType.SYSTEM_DATE, Constants.SysConfigID.SYSTEM_DATE).Trim() + " " + DateTime.Now.ToLongTimeString(), new CultureInfo("en-US"));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        protected string GetipAddress()
        {
            string IP4Address = String.Empty;

            foreach (System.Net.IPAddress IPA in System.Net.Dns.GetHostAddresses(System.Web.HttpContext.Current.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (System.Net.IPAddress IPA in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
            //string strHostName = Request.UserHostAddress;
            //return strHostName;
        }

        public class SessionTimeoutAttribute : ActionFilterAttribute
        {

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var login = new LoginModel();
                string msg = "";
                using (var baseCon = new BaseController())
                {

                    msg = baseCon.CheckSession();
                    if (!string.IsNullOrEmpty(msg))
                    {
                        filterContext.Result = new RedirectResult(VirtualPathUtility.ToAbsolute("~/Home/Expire") + "?msg=" + msg);
                        return;
                    }
                    var s = filterContext.HttpContext.Session;
                }
                base.OnActionExecuting(filterContext);
            }

        }

        public string CheckSession()
        {
            string msg = "";
            try
            {
                //ตรวจสอบ Session
                var sessionLogin = (LoginModel)System.Web.HttpContext.Current.Session[Constants.SESSION_KEY.LoginInfo];
                if (sessionLogin == null || sessionLogin.login == null || sessionLogin.login.currentId.IsNullOrEmpty())
                {
                    msg = Constants.Msg.SessionExp;
                }
                return msg;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public virtual string PartialView(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
        public void DirectoryManagement(string sourceDurectiry)
        {
            try
            {
                if (!Directory.Exists(sourceDurectiry))
                    Directory.CreateDirectory(sourceDurectiry);
            }
            catch
            {
                throw;
            }
        }
        //public virtual ActionResult Download(string id)
        //{
        //    try
        //    {

        //        var file = (FileInfo)Session[id];
        //        file
        //        if (!file.IsNullOrEmpty())
        //        {
        //            Stream stream = new MemoryStream(file.fileBytes);
        //            return File(stream, "application/pdf");
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public VIEW_M_TRANS_CODE GetTransCodeByCode(string transcode)
        {
            try
            {
                var returnVal = db.VIEW_M_TRANS_CODE.Where(x => x.TRANS_CODE == transcode).FirstOrDefault();
                return returnVal ?? new VIEW_M_TRANS_CODE();
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult CalVatAmt(string transCode, decimal amount)
        {
            JsonResult result = new JsonResult();
            try
            {
                decimal vat = 0;
                decimal amountExVat = amount;
                var TotalAmount = amount;
                if (!transCode.IsNullOrEmpty())
                {
                    var rate = CommonMethods.GetVatRate();
                    //var objTrans = GetTransCodeByCode(transCode);
                    //if (objTrans.VAT_FLAG ?? false)
                    //{
                        amountExVat = amount * 100 / (100 + rate) ;
                    //}
                }
                vat = TotalAmount - amountExVat;

                result = Json(new
                {
                    amountExVat = amountExVat.ToString("N2"),
                    vat = vat.ToString("N2"),
                    totalAmount = TotalAmount.ToString("N2")
                },  JsonRequestBehavior.AllowGet);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult DownloadFile(string file)
        {
            //string filename = "File.pdf";
            //string filepath = AppDomain.CurrentDomain.BaseDirectory + "/Path/To/File/" + filename;
            byte[] filedata = System.IO.File.ReadAllBytes(file);
            string contentType = MimeMapping.GetMimeMapping(file);

            //var cd = new System.Net.Mime.ContentDisposition
            //{
            //    FileName = filename,
            //    Inline = true,
            //};

            //Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }

        public byte[] MergePdf(List<string> pdfFiles, string outputFileName)
        {
            List<string> lostFile = new List<string>();
            string outputDir = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()), string.Concat(System.Web.Hosting.HostingEnvironment.MapPath(Constants.MapPath.TempPdf)));
            string fileName = outputDir + @"\" + outputFileName + ".pdf";
            try
            {
                byte[] mergeFileByte = PdfMerger.MergeFiles(pdfFiles, ref lostFile);
                if (mergeFileByte != null)
                {
                    if (!mergeFileByte.Length.Equals(0))
                    {
                        if (!Directory.Exists(outputDir))
                            Directory.CreateDirectory(outputDir);

                        using (FileStream fs = new FileStream(fileName, FileMode.Create))
                        {
                            fs.Write(mergeFileByte, 0, mergeFileByte.Length);
                            fs.Close();
                        }
                    }
                }

                //for (int fileCounter = 0; fileCounter < pdfFiles.Count; fileCounter++)
                //{
                //    if (System.IO.File.Exists(pdfFiles[fileCounter]))
                //        System.IO.File.Delete(pdfFiles[fileCounter]);
                //}
                //_mergePath.Add(fileName);

                byte[] FileByte = System.IO.File.ReadAllBytes(fileName);
                return FileByte;
            }
            catch (Exception ex)
            {
                throw ex;
                //summaryLogData.AppendLine("Error when merged file Name:: " + outputFileName);
                //summaryLogData.AppendLine("message:: " + ex.Message);
                //summaryLogData.AppendLine("stacktrack:: " + ex.StackTrace);
            }
        }

        public ActionResult ModalSearch(PopAdditionalSearchViewModel model, string RadioSearch)
        {
            try
            {

                if (model.search.IsNullOrEmpty())
                {
                    //db.MERGE_DATA();
                    model.RadioSearch = BindSearchType_Adjustment();
                    return PartialView("../Shared/ModalSearch", model);
                }
                else
                {
                  
                    string contractCode = null;
                    string name = null;
                    string chassicNo = null;
                    string licensePlate = null;
                    string idCard = null;
                    switch (RadioSearch)
                    {
                        case Constants.SearchCriteria.ContractNo_Value:
                            contractCode = model.search;
                            break;
                        case Constants.SearchCriteria.CustomerName_Value:
                            name = model.search;
                            break;
                        case Constants.SearchCriteria.ChassisNo_Value:
                            chassicNo = model.search;
                            break;
                        case Constants.SearchCriteria.LicensePlate_Value:
                            licensePlate = model.search;
                            break;
                        case Constants.SearchCriteria.IdCard_Value:
                            idCard = model.search;
                            break;
                        default:
                            break;
                    }

                    var query = (from con in db.T_CONTRACT
                                       join cus in db.VIEW_M_CUST_CUSTOMER on con.CUSTOMER_CODE equals cus.CUSTOMER_CODE
                                       join rca in db.VIEW_FRONTEND_R_CONT_ASSET on con.CONT_CODE equals rca.CONT_CODE into g1
                                       from rca in g1.DefaultIfEmpty()

                                       join veh in db.VIEW_FRONTEND_M_ASSET_VEHICLE on new {p1 = (rca.ASSET_ID ?? 0), p2 = Constants.AssetType.Vehicle } equals new {p1 = veh.ASSET_ID, p2 = veh.ASSET_TYPE_ID } into g2
                                       from veh in g2.DefaultIfEmpty()

                                       where (con.CONT_CODE.Contains(contractCode) || contractCode == null)
                                       && ((cus.FIRST_NAME + " " + cus.LAST_NAME).Contains(name) || name == null)
                                       && (veh.CHASSIS_NO.Contains(chassicNo) || chassicNo == null)
                                       && (veh.LICENSE_NO.Contains(licensePlate) || licensePlate == null)
                                       && (cus.ID_CARD_NO.Contains(idCard) || idCard == null)
                                       && (con.COMP_CODE == SessionLoginInfo.login.compCode && con.COMP_BRANCH_CODE == SessionLoginInfo.login.branchCode)

                                       select new { con, cus, veh}
                                        ).ToList();

                    List<ModalSearchtbDetail> viewModel = new List<ModalSearchtbDetail>();
                    foreach(var data in query)
                    {
                        viewModel.Add(new ModalSearchtbDetail
                        {
                            ContractCode = data.con.CONT_CODE,
                            CustIDCard = data.cus.ID_CARD_NO,
                            CustName = CommonMethods.GetCustomerFullName(data.cus.CUSTOMER_CODE),
                            LoanTypeDesc = CommonMethods.GetMasterDescription(data.con.LOAN_TYPE_ID, Constants.MasterType.LoanType),
                            ChassisNo = data.veh?.CHASSIS_NO,
                            LicenseNo = data.veh?.LICENSE_NO
                        });
                    }

                    return Json(new
                    {
                        r = Constants.Result.True,
                        msg = string.Format("ค้นพบข้อมูลทั้งหมด {0} รายการ", query.Count),
                        partial = PartialView(this, "../Shared/_tbModalSearch", viewModel)
                    }
                    , JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ListItem> BindSearchType_Adjustment()
        {
            try
            {
                List<ListItem> rbtn = new List<ListItem>();
                rbtn.Add(new ListItem(Constants.SearchCriteria.ContractNo_Text, Constants.SearchCriteria.ContractNo_Value));
                rbtn.Add(new ListItem(Constants.SearchCriteria.ChassisNo_Text, Constants.SearchCriteria.ChassisNo_Value));
                rbtn.Add(new ListItem(Constants.SearchCriteria.CustomerName_Text, Constants.SearchCriteria.CustomerName_Value));
                rbtn.Add(new ListItem(Constants.SearchCriteria.LicensePlate_Text, Constants.SearchCriteria.LicensePlate_Value));
                rbtn.Add(new ListItem(Constants.SearchCriteria.IdCard_Text, Constants.SearchCriteria.IdCard_Value));
                return rbtn;
            }
            catch
            {
                throw;
            }
        }

        public PartialViewResult ModalPayment()
        {
            try
            {
                PopPaymentViewModel viewModel = new PopPaymentViewModel();
                return PartialView("../Shared/ModalPayment", viewModel);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public JsonResult BindBankBranch(string code)
        {
            try
            {
                var returnValue = db.VIEW_M_BANK_BRANCH
                                    .Where(x => x.BANK_ID == code).Select(x => new SelectListItem { Value = x.BANKBRANCH_ID, Text = x.NAME_THA })
                                    .ToList();

                return Json(returnValue);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}