using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace SmallCode.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BaseController : Controller
    {
        public User CurrentUser { set; get; }

        public IDictionary<string, object> Parameters { set; get; }

        public SMContext DB { get { return HttpContext.RequestServices.GetService<SMContext>(); } }

        public override void OnActionExecuting(ActionExecutingContext context)

        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUser = DB.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
                ViewBag.UserCurrent = CurrentUser;
            }
            Parameters = new Dictionary<string, object>();
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
