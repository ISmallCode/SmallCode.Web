using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.DataModels;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models;


namespace SmallCode.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Inject]
        public IMaterialsService materialsService { set; get; }

        [Inject]
        public IUserService userService { set; get; }


        public IActionResult Index()
        {

            List<Materials> materialses = new List<Materials>();
            List<User> users = new List<User>();
            materialses = materialsService.GetLatest10();
            users = userService.GetLatest10();

            //动态消息
            List<DynamicModel> infoes = new List<DynamicModel>();
            foreach (var item in materialses)
            {
                DynamicModel model = new DynamicModel();
                infoes.Add(model);
                model.Title = item.Title;
                model.Id = item.Id;
                model.CreateDate = item.CreateDate;
                model.Url = "/Materials/Show/" + item.Id;
            }

            //动态用户信息
            List<DynamicModel> userList = new List<DynamicModel>();
            foreach (var item in users)
            {
                DynamicModel model = new DynamicModel();
                userList.Add(model);
                model.Title = item.UserName;
                model.Id = item.Id;
                model.CreateDate = item.CreatedDate;
                model.Url = "/User/Show/" + item.Id;
            }

            ViewBag.Infoes = infoes;
            ViewBag.Users = userList;

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
