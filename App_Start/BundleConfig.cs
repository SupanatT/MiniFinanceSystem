using System.Web;
using System.Web.Optimization;

namespace MiniFinance
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //------------------------------------------------------------------------------------------------------//
            #region Styles
            bundles.Add(new StyleBundle("~/Content/Common").Include(
                        "~/Assets/bootstrap/css/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/plugins.css",
                        "~/Content/layout.css",
                        "~/Content/components.min.css",

                        "~/Content/lightblue.css",
                        //"~/Content/default.css", //สำหรับเริ่มต้น
                        "~/Assets/datatables/datatables.min.css",
                        "~/Assets/datatables/plugins/bootstrap/datatables.bootstrap.css",
                        //"~/Assets/datatables/button/css/jquery.dataTables.min.css",
                        //"~/Assets/datatables/button/css/buttons.dataTables.css",
                        //"~/Assets/datatables/button/css/buttons.jqueryui.min.css",
                        //"~/Assets/datatables/button/css/dataTables.bootstrap.min.css",
                        //"~/Assets/datatables/button/css/dataTables.foundation.min.css",
                        //"~/Assets/datatables/button/css/dataTables.jqueryui.min.css",

                        "~/Assets/bootstrap-modal/css/bootstrap-modal.css",
                        "~/Assets/simple-line-icons/simple-line-icons.min.css",
                        "~/Assets/font-awesome/css/font-awesome.min.css",

                        "~/Assets/bootstrap-fileinput/fileinput.css",
                        "~/Assets/nouislider/nouislider.css",
                        "~/Assets/bootstrap-toastr/toastr.css",



                        "~/Content/StyleCustom.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                        "~/Assets/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                        "~/Assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css",
                        "~/Assets/bootstrap-datepicker-thai/css/datepicker.css"
                        ));
            bundles.Add(new ScriptBundle("~/Content/select2").Include(
                        "~/Assets/select2/css/select2.css",
                        "~/Assets/select2/css/select2-bootstrap.min.css"
                        ));
            #endregion
            //------------------------------------------------------------------------------------------------------//
            #region Scripts
            bundles.Add(new ScriptBundle("~/Script/Common").Include(
                        "~/Scripts/jquery-1-12-4.js",
                        "~/Assets/jquery-validation/js/jquery.validate.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Assets/bootstrap/js/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/jquery.blockui.min.js",
                        "~/Scripts/app.js",
                        "~/Scripts/layout.js",
                        "~/Scripts/moment.min.js",
                        "~/Assets/bootstrap-modal/js/bootstrap-modal.js",
                        "~/Assets/bootstrap-modal/js/bootstrap-modalmanager.js",

                        "~/Assets/datatables/datatables.min.js", //อยู่บนจะสามารถใช้งาน html5 export ได้

                        "~/Assets/datatables/button/js/jquery.dataTables.min.js",
                        "~/Assets/datatables/button/js/buttons.html5.js",
                        "~/Assets/datatables/button/js/buttons.print.min.js",
                        "~/Assets/datatables/button/js/buttons.colVis.min.js",
                        "~/Assets/datatables/button/js/dataTables.buttons.min.js",


                        "~/Assets/datatables/plugins/bootstrap/datatables.bootstrap.js",


                        //"~/Assets/datatables/button/js/dataTables.bootstrap.js",
                        //"~/Assets/datatables/button/js/buttons.bootstrap.min.js",

                        //"~/Assets/datatables/button/js/dataTables.jqueryui.min.js",
                        "~/Assets/bootstrap-confirmation/bootstrap-confirmation.js",
                        "~/Assets/bootbox/bootbox.min.js",
                        "~/Assets/bootstrap-toastr/toastr.js",

                        "~/Assets/bootstrap-fileinput/plugins/piexif.min.js",
                        "~/Assets/bootstrap-fileinput/plugins/sortable.min.js",
                        "~/Assets/bootstrap-fileinput/plugins/purify.min.js",
                        "~/Assets/bootstrap-fileinput/fileinput.js",
                        "~/Assets/clipboardjs/clipboard.min.js",
                        "~/Assets/nouislider/nouislider.js",
                        "~/Assets/nouislider/wNumb.min.js",

                        "~/Scripts/ScriptCustom.js"
           ));
            bundles.Add(new ScriptBundle("~/Script/datepicker").Include(

                        "~/Assets/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                        "~/Scripts/components-date-time-pickers.js",
                        "~/Assets/bootstrap-datepicker-thai/js/bootstrap-datepicker.js",
                        "~/Assets/bootstrap-datepicker-thai/js/bootstrap-datepicker-thai.js",
                        "~/Assets/bootstrap-datepicker-thai/js/locales/bootstrap-datepicker.th.js"
           ));
            #endregion
            //------------------------------------------------------------------------------------------------------//
            #region Custom
            bundles.Add(new ScriptBundle("~/bundles/autonumeric").Include(
                        "~/Assets/autoNumeric/autoNumeric-min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/select2").Include(
                        "~/Assets/select2/js/select2.full.js",
                        "~/Assets/select2/js/select2.min.js",
                        "~/Assets/select2/components-select2.js"
                        ));
            #endregion
            //------------------------------------------------------------------------------------------------------//
        }
    }
}
