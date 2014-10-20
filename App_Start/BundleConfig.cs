using System.Web;
using System.Web.Optimization;

namespace InnDocs.iHealth.Web.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Common scripts.
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.7.1.min.js",
                "~/Scripts/jquery-ui-1.9.2.js",
                "~/Scripts/jquery.simplemodal.js",
                "~/Scripts/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery1.1").Include(
               "~/Scripts/jquery-1.7.1.min.js",
               "~/Scripts/jquery-ui-1.9.2.js",
               "~/Scripts/jquery.simplemodal.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));
            //
            // jagadeesh added 
            bundles.Add(new StyleBundle("~/bundles/inndocsnewstyles").Include(
                "~/Content/basic_ie.css",
                "~/Content/User.css",
                "~/Content/ui.dropdownchecklist.standalone.css",
                "~/Content/trend.css",
                "~/Content/basic.css",
                "~/Content/dd.css",
                "~/Content/Feedback.css",
                "~/Content/resolution.css",
                "~/Content/forchat.css",
                "~/Content/ui.dropdownchecklist.standalone.css"));




            //Before login.
            bundles.Add(new ScriptBundle("~/bundles/inndocshome").Include(
                "~/CustomScripts/inndocsscript.js",
                 "~/CustomScripts/FooterLinks.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxfns").Include(
                "~/CustomScripts/ajax-fns.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxforgotpwd").Include(
               "~/CustomScripts/Forgotpwd.js"));
            bundles.Add(new StyleBundle("~/bundles/inndocshstyles").Include(
                "~/Content/Site.css",
                "~/Content/basic.css",
                "~/Content/basic_ie.css"));

            //After login.
            bundles.Add(new StyleBundle("~/bundles/inndocsustyles").Include(
                "~/Content/User.css",
                "~/Content/basic_ie.css",
                "~/Content/basic.css",
                "~/Content/dd.css"));

            bundles.Add(new ScriptBundle("~/bundles/filentime").Include(
                "~/Scripts/fileDownload.js",
                "~/Scripts/jquery-ui-timepicker-addon.js"));

            bundles.Add(new ScriptBundle("~/bundles/selectnchange").Include(
                "~/CustomScripts/selected-tab.js",
                "~/CustomScripts/change-pswd.js"));
            bundles.Add(new ScriptBundle("~/bundles/ajaxfun").Include(
              "~/CustomScripts/ajax-log.js"));
            bundles.Add(new ScriptBundle("~/bundles/ajaxuserfns").Include(
                "~/CustomScripts/ajax-user-fns.js"));
            bundles.Add(new ScriptBundle("~/bundles/ajaxchngpwd").Include(
                "~/CustomScripts/Ajax-chngpwd.js"));

            bundles.Add(new ScriptBundle("~/bundles/PCode").Include(
                "~/CustomScripts/AdminPCode.js"));

            bundles.Add(new ScriptBundle("~/bundles/PerAccUser").Include(
                "~/CustomScripts/PAcc-User.js"));
            bundles.Add(new ScriptBundle("~/bundles/ajaxconfirmaccount").Include(
           "~/CustomScripts/confirmaccount.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxfnsj").Include(
            "~/CustomScripts/createaccount.js"));

            // sandeep added
            bundles.Add(new ScriptBundle("~/bundles/AccountInfo").Include(
                "~/CustomScripts/personal-account.js",
                "~/CustomScripts/edit-user.js"));

            bundles.Add(new ScriptBundle("~/bundles/Branch").Include(
                "~/CustomScripts/HospAdmin.js"));

            bundles.Add(new ScriptBundle("~/bundles/CreateDepartment").Include(
               "~/CustomScripts/createdepartments.js"));

            bundles.Add(new ScriptBundle("~/bundles/Mobilevalidation").Include(
              "~/CustomScripts/Mobilevalidation.js"));

            bundles.Add(new ScriptBundle("~/bundles/mouse").Include("~/Scripts/jquery.mousewheel.js",
                        "~/CustomScripts/timeline.js"));



            #region homepage

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js",

                        "~/Scripts/jquery.md5.min.js",
                        "~/UIScripts/function.js",
                        "~/UIScripts/control.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site1.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/bundles/style").Include("~/Content/bootstrap.css"));

            // BundleTable.EnableOptimizations = true;
            #endregion


            BundleTable.EnableOptimizations = true;

        }
    }
}