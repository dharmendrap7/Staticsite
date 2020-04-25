using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staticsite.App_Start
{
    public class SessionTimeOut: ActionFilterAttribute
    {
       
    
          public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;
                if (HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }

