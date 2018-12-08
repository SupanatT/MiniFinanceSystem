//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using MiniFinance.Common;
using MiniFinance.Models;
using MiniFinance.Models.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFinance.Controllers
{
  
    public class HomeController : BaseController
    {
        [SessionTimeout]
        public ActionResult Index()
        {
           // var xx = SessionLoginInfo.login.branchCode;
            return View();
        }



        public ActionResult Login()
        {
            LoginModel objStrLoginInfo;

            List<LoginModel.Menu> lsMenu = new List<LoginModel.Menu>();
            LoginModel.Login objLogin = new LoginModel.Login();
            LoginModel.Menu objMenu;


            try
            {
                //ฝั่ง Dev
                if ("dev".Equals(Constants.AppSetting.WebMode))
                {
                    objLogin = new LoginModel.Login();
                    objLogin.currentId = "1";
                    objLogin.username = "Admin";
                    objLogin.fullName = "Administrator";
                    objLogin.ipaddress = GetipAddress();
                    //SessionLoginInfo = objStrLoginInfo;
                    objLogin.branchCode = Constants.AppSetting.BranchCode;
                    objLogin.compCode = Constants.AppSetting.CompanyCode;

                    var lsSpMenu = db.LEFT_MENU_DEV.ToList();
                    foreach (var menu in lsSpMenu)
                    {
                        objMenu = new LoginModel.Menu(menu);
                        lsMenu.Add(objMenu);
                    }
                }
                //ฝั่ง Portal
                else
                {
                    //var dtUserInfo = GetLoginInfo(Current_ID);
                    //if (!dtUserInfo.IsNull() && dtUserInfo.Count > 0)
                    //{
                    //    foreach (var drUser in dtUserInfo)
                    //    {
                    //        objLogin = new LoginModel.Login(Db, drUser, Current_ID);
                    //    }

                    //}
                    //var dtMenu = GetMenu(Current_ID);
                    //dsService.PermissionDataTable dtPerm;
                    //foreach (var drMenu in dtMenu)
                    //{
                    //    dtPerm = GetPermission(Current_ID, drMenu.Program_Code);
                    //    objMenu = new LoginModel.Menu(drMenu, dtPerm);
                    //    lsMenu.Add(objMenu);
                    //}
                }

                SessionLoginInfo = new LoginModel(objLogin, lsMenu);

            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Expire(string msg)
        {
            var url = Constants.AppSetting.PortalLogin;
            Response.StatusCode = 999;
            Response.StatusDescription = msg;
            Response.AddHeader("url", url);
            ViewBag.msg = msg;
            ViewBag.url = url;
            Session.RemoveAll();
            return PartialView(ViewBag);
        }


    }
}