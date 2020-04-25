using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Staticsite.Controllers
{
    public partial class BaseController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected virtual async Task<ActionResult> SendEmail(string subject, string body)
        {
            await UserManager.SendEmailAsync(UserId.Value.ToString(), subject, body);
            return null;
        }

        protected Guid? UserId
        {
            get
            {
                if (!Request.IsAuthenticated)
                    return null;

                var claimsIdentity = HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
                var claim = claimsIdentity?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                if (claim != null)
                    return new Guid(claim.Value);

                return null;
            }
        }

        //protected User LoggedInUser
        //{
        //    get
        //    {
        //        var token = (AppSessionToken)Session[Constants.SessionKeyUser];

        //        return token != null && token.IsValid ? token.User : null;
        //    }
        //}

        protected bool IsMyself(Guid employeeId)
        {
            return UserId.HasValue && UserId.Value == employeeId;
        }

        //public string Session1()
        //{
        //    var tokenpre2 = SessionTokenGenerationServices.SessionToken1;
        //    //var tokenpre3 = tokenpre2.Replace("-", string.Empty);
        //    return tokenpre3;
        //}

        protected string SessionToken
        {
            get
            {
                //SessionTokenGenerationServices svc = new SessionTokenGenerationServices(
                var token = System.Web.HttpContext.Current.Session["SessionToken"] as string;
                //UserSessionModel userSession = new UserSessionModel();
                //SessionTokenGenerationServices session = new SessionTokenGenerationServices();
                //var token = session.SaveUserSession(userSession).Entity.Session;
                //var token = (AppSessionToken)Session[Constants.SessionKeyUser];

                return token != null ? token : null;
                //return token;
            }
        }

        public object CommonHelper { get; private set; }

        //protected void SetSessionToken(AppSessionToken token)
        //{
        //    //token.SessionExpiry = DateTime.UtcNow.AddMinutes(2);
        //    Session[Constants.SessionKeyUser] = token;
        //}

        //protected HttpCookie AttemptSignInThroughCookie()
        //{
        //    var userServices = new UserServices();

        //    var cookie = Request.LoadLoginCookie();

        //    if (cookie != null)
        //    {
        //        var cookieValues = cookie.Value.Split('|');
        //        var email = cookieValues[0];
        //        var cookieHash = cookieValues[1];

        //        var userSession = userServices.SignInThroughCookie(email, cookieHash, Request.GetClientIpAddress(), Request.UserAgent);

        //        if (userSession != null && userSession.SessionExpiry > DateTime.UtcNow)
        //        {
        //            SetSessionToken(ErpSessionToken.Create(userSession));

        //            TimeTrackingJobs.UpdateRunningJobSessionToken(userSession.User.Email, userSession.SessionToken);

        //            return cookie;
        //        }
        //    }

        //    return null;
        //}

        //protected bool HasFunction(eSecurityFunction securityFunction)
        //{
        //    var sessionToken = Session.GetErpSessionToken();

        //    if (sessionToken == null || !sessionToken.IsValid)
        //        return false;

        //    return sessionToken.HasFunction(securityFunction);
        //}

        //    protected ActionResult RedirectToLocal(string returnUrl)
        //    {
        //        if (Url.IsLocalUrl(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }

        //        return RedirectToAction(MVC.Account.ActionNames.EnterPin, MVC.Account.Name);
        //    }

        //    protected ActionResult RedirectToObjectNotFound()
        //    {
        //        return RedirectToAction(MVC.Errors.ActionNames.Error405ObjecteNotFound, MVC.Errors.Name);
        //    }

        //    protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //    {
        //        base.OnActionExecuting(filterContext);

        //        if (Request.Url != null) CommonHelper.SetCompanyName(Request.Url.Host);

        //        var isAnnonymous = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

        //        //if (!isAnnonymous && SessionToken == null && !(filterContext.Controller is AccountController) && !(filterContext.Controller is ErrorsController))
        //        //{
        //        //    filterContext.Result = base.RedirectToAction(MVC.Account.ActionNames.Login, MVC.Account.Name, new { returnUrl = filterContext.HttpContext.Request.RawUrl });
        //        //}
        //    }

        //    private ActionResult JsonSuccess(string displayMessage = null)
        //    {
        //        TempData["Success"] = displayMessage ?? "The data has been saved successfully";

        //        Response.StatusCode = (int)HttpStatusCode.OK;

        //        return Content(TempData["Success"].ToString(), MediaTypeNames.Text.Plain);
        //    }

        //    protected ActionResult JsonError(List<string> errorMessages)
        //    {
        //        TempData["Error"] = errorMessages;

        //        Response.TrySkipIisCustomErrors = true;
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;

        //        return Json(TempData["Error"]);
        //    }

        //    protected ActionResult JsonData<T>(RequestResult<T> requestResult, string displayMessage = null) where T : class
        //    {
        //        if (requestResult != null)
        //        {
        //            if (requestResult.HasError)
        //                return JsonError(requestResult.ErrorMessages);

        //            return JsonSuccess(displayMessage);
        //        }

        //        return Json(displayMessage, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}