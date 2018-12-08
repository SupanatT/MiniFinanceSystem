using MiniFinance.Common;
using MiniFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: Default
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult _PageMenu()
        {
            try
            {
                var lsModel = SessionLoginInfo.menu ?? new List<LoginModel.Menu>();
                return PartialView(lsModel);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult _PageHeader()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult _PageTopMenu()
        {
            try
            {

                return PartialView(SessionLoginInfo.login);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult _DatePicker(string id)
        {
            ViewBag.Id = id;
            return PartialView();
        }

        public ActionResult _Loading()
        {
            return PartialView();
        }

        public ActionResult _Modal()
        {
            return PartialView();
        }

        public ActionResult _PageBreadcrumb(string caption, string detail, string parentDetail)
        {
            try
            {
                ViewBag.Caption = caption;
                ViewBag.Detail = detail;
                ViewBag.ParentDetail = parentDetail;
                return PartialView(ViewBag);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public JsonResult LogOut()
        {
            Session.RemoveAll();
            return Json(new { url = Constants.AppSetting.PortalLogin });


        }

        //public JsonResult Setting(string param)
        //{
        //    CallBatch(param);

        //    return Json(new { });
        //}
    }
}