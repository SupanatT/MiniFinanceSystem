using MiniFinance.Common;
using MiniFinance.Models.BusinessLogicModel;
using MiniFinance.Models.DataModel;
using MiniFinance.ViewModel.WriteOffManual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Controllers
{
    public class WriteOffManualController : BaseController
    {
        // GET: WriteOffManual
        WriteOffManualModel bllModel = new WriteOffManualModel();

        [SessionTimeout]
        public ActionResult Index()
        {
            return View(new MainDataViewModel());
        }

        public ActionResult Search(MainDataViewModel viewModel)
        {
            var cshObj = db.VIEW_CASH_RECEIVE_T_CONTRACT.FirstOrDefault(x => x.Contract_Code == viewModel.ContractCode);
            if(cshObj?.Contract_Status == Constants.ContractStatus.Close || cshObj?.Contract_Status == Constants.ContractStatus.Cancel)
            {
                return Json(new
                {
                    r = Constants.Result.False,
                    msg = "เลขที่สัญญา " + viewModel.ContractCode + " ถูกปิดหรือถูกยกเลิกไปแล้ว"
                }, JsonRequestBehavior.AllowGet);
            }

            bllModel.BindData(viewModel);

            return Json(new
            {
                r = Constants.Result.True,
                //msg = "ไม่พบเลขที่สัญญาในระบบ"
                result = PartialView(this, "Index", viewModel)
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ModalSearchSelect(string code)
        {
            var model = new MainDataViewModel();
            model.ContractCode = code;

            return RedirectToAction("Search", model);
        }

        [HttpPost]
        public ActionResult Save(MainDataViewModel viewModel)
        {
            bool result = false;
            string message = "";

            try
            {
                bllModel.Save(db, viewModel, SessionLoginInfo.login);

                bllModel.CallWriteOffApi(viewModel, SessionLoginInfo.login);

                db.SaveChanges();

                result = true;
                message = Constants.Message.SaveSuccess;

                return Json(new { result = result, msg = message }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}