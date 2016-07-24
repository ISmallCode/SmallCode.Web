using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.DataModels;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models;
using SmallCode.Web.Extensions;
using CommonMark;

namespace SmallCode.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Inject]
        public IMaterialsService materialsService { set; get; }

        [Inject]
        public IUserService userService { set; get; }

        [Inject]
        public IArticleService articleService { set; get; }


        public IActionResult Index()
        {

            List<HomeInfoModel> homeInfoes = new List<HomeInfoModel>();
            List<Article> articles = new List<Article>();
            List<Materials> materialses = new List<Materials>();
            List<User> users = new List<User>();
            materialses = materialsService.GetLatest10();
            users = userService.GetLatest10();




            //动态消息
            List<DynamicModel> infoes = new List<DynamicModel>();
            articles = articleService.GetLatest10();
            foreach (var item in articles)
            {
                string[] imageUrls = CommonMarkConverter.Convert(item.Description).GetHtmlImageUrlList();
                HomeInfoModel homeInfo = new HomeInfoModel();
                homeInfo.Title = item.Title;
                homeInfo.Description = item.Description.SubString(100, "......");
                homeInfo.CreateDate = item.CreateDate;
                homeInfo.Category = "原创文章";
                homeInfo.Browses = item.Browses;
                homeInfo.Url = "/Article/Show/" + item.Id;
                homeInfo.ImageUrl = imageUrls.Count() > 0 ? imageUrls[0] : null;
                homeInfoes.Add(homeInfo);

            }
            foreach (var item in materialses)
            {
                DynamicModel model = new DynamicModel();
                HomeInfoModel homeInfo = new HomeInfoModel();
                infoes.Add(model);
                homeInfoes.Add(homeInfo);

                model.Title = item.Title;
                model.Id = item.Id;
                model.CreateDate = item.CreateDate;
                model.Url = "/Materials/Show/" + item.Id;

                homeInfo.Title = item.Title;
                homeInfo.Description = item.Description.SubString(100, "......");
                homeInfo.CreateDate = item.CreateDate;
                homeInfo.Category = "学习资料";
                homeInfo.Browses = item.Browses;
                homeInfo.Url = "/Materials/Show/" + item.Id;
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
                model.Url = "/Account/Show/" + item.UserName;
            }

            ViewBag.Infoes = infoes;
            ViewBag.Users = userList;
            ViewBag.HomeInfoes = homeInfoes.OrderByDescending(x => x.CreateDate).ToList();



            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
