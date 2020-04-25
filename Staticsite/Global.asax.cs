using Conquerorhub.SDK.Services;
using Staticsite.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Staticsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string con = ConfigurationManager.ConnectionStrings["ConquerorHubEntities2"].ConnectionString;
        protected void Application_Start()
        {
             
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SqlDependency.Start(con);//-------------------------------
        }
        protected async void Session_Start(object sender, EventArgs e)
        {
            NotificationComponent NC = new NotificationComponent();
            ApplicationMandatoryService obj = new ApplicationMandatoryService();
            var userId = HttpContext.Current.Session["UserId"] as string;
            if (userId != null)
            {
                var data = await obj.GetLastLogindb(userId);
                var currentTime = data.CreatedDateandTime;
                HttpContext.Current.Session["LastUpdated"] = currentTime;
                NC.RegisterNotification(currentTime);
            }
        }
        protected void Session_End(object sender, EventArgs e)
        {

        }
        protected void Application_End()
        {
            //here we will stop Sql Dependency  
            SqlDependency.Stop(con);
        }
    }
}
