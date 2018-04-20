﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NorthwindPL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/Content", "*.css",true));
            BundleTable.Bundles.Add(new ScriptBundle("~/Scripts/js").IncludeDirectory("~/Scripts", "*.js",true));
            BundleTable.EnableOptimizations = true;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}