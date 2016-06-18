using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmallCode.Web.Controllers
{
    public class BaseController : Controller
    {
        public User CurrentUser { set; get; }

        public SCContext db { get { return HttpContext.RequestServices.GetService<SCContext>(); } }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
                ViewBag.CurrentUser = CurrentUser;
            }
            ViewBag.CurrentUser = CurrentUser;
            base.OnActionExecuting(context);
        }
    }
}
