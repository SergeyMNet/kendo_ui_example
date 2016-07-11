using System.Web;
using System.Web.Optimization;

namespace TelerikMvcApp2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/bootstrap.css"
                      ));

            bundles.Add(new StyleBundle("~/Kendo_global/css").Include(
                      "~/Content/kendo/2016.1.112/kendo.common.min.css",
                      "~/Content/kendo/2016.1.112/kendo.mobile.all.min.css",
                      "~/Content/kendo/2016.1.112/kendo.dataviz.min.css",
                      "~/Content/kendo/2016.1.112/kendo.default.min.css",
                      "~/Content/kendo/2016.1.112/kendo.dataviz.default.min.css",
                      "~/Content/kendo/2016.1.112/kendo.rtl.min.css"));

            bundles.Add(new ScriptBundle("~/Kendo_global/scripts").Include(
                      "~/Scripts/kendo/2016.1.112/jquery.min.js",
                      "~/Scripts/kendo/2016.1.112/jszip.min.js",
                      "~/Scripts/kendo/2016.1.112/kendo.all.min.js",
                      "~/Scripts/kendo/2016.1.112/kendo.aspnetmvc.min.js",
                      "~/Scripts/kendo.modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/Kendo_page_test/scripts").Include(
                      "~/Scripts/Kendo_Test_Page/FilterScripts.js",
                      "~/Scripts/Kendo_Test_Page/GridScripts.js",
                      "~/Scripts/Kendo_Test_Page/TreeScripts.js",
                      "~/Scripts/Kendo_Test_Page/FileUploaderScript.js",
                      "~/Scripts/Kendo_Test_Page/ServerMessageScript.js"));
        }
    }
}
