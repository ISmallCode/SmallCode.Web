using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmallCode.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.DataModels;

namespace SmallCode.Web.Controllers
{
    public class AccountController : BaseController
    {
        [Inject]
        public IUserService userService { set; get; }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回登陆页面 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 执行登录
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            JsonResult<object> model = new JsonResult<object>();

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                model.Message = "用户名或者密码不能为空";
                model.Status = ResultStatus.OK;
            }
            else
            {
                User user = userService.Login(Username, Password);
                if (userService.IsSuccess)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, Username));

                    await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                              new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                    model.Message = "登录成功";
                    model.Status = ResultStatus.OK;
                }
                else
                {
                    model.Message = "登录失败";
                    model.Status = ResultStatus.Error;
                }
            }
            return Json(model);
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index");
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 执行注册
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string Username, string Password, string Email)
        {
            JsonResult<object> model = new JsonResult<object>();
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
            {
                model.Status = ResultStatus.OK;
                model.Message = "输入的信息不能为空";
            }
            User user = new User();
            user.Email = Email;
            user.Password = Password;
            user.UserName = Username;

            await userService.Save(user);

            model.Status = ResultStatus.OK;
            model.Message = "注册成功";
            return Json(model);
        }
    }
}
