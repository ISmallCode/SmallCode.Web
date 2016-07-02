using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Models;
using SmallCode.Pager;
using SmallCode.Web.DataModels;
//using Microsoft.AspNetCore.SignalR.Infrastructure;
//using Microsoft.AspNetCore.SignalR;
using SmallCode.Web.Hubs;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Web.Filters;
using SmallCode.Web.Services;

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        [Inject]
        public IUserService userService { set; get; }

        // GET: /<controller>/
        public IActionResult Index(string UserName, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<User> pageData = userService.GetUserByPage(UserName, PageIndex, PageSize);
            return View(pageData);
        }


        #region 修改 增加用户
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string UserName, string Email)
        {
            User user = new User
            {
                UserName = UserName,
                Password = "123456",
                Email = Email,
                Role = Role.管理员,
            };

            await userService.Save(user);

            DynamicModel dModel = new DynamicModel();
            dModel.CreateDate = user.CreatedDate;
            dModel.Title = user.UserName;
            dModel.Url = "/User/Show" + user.Id;
            dModel.Id = user.Id;

            ///将更新发到全部的客户端  这里用到了DI
            //IConnectionManager conn = HttpContext.RequestServices.GetService<ConnectionManager>();
            //IHubContext context = conn.GetHubContext<SMHub>();
            //context.Clients.All.Send("user", dModel);
            return View();
        }

        #endregion


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            userService.Remove(id);
            ReturnResult<object> model = new ReturnResult<object>();
            model.Status = userService.IsSuccess ? "success" : "fail";
            model.ReturnMsg = userService.ReturnMsg;
            return Json(model);
        }

    }
}
