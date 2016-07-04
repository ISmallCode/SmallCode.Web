using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models;
using SmallCode.Pager;

namespace SmallCode.Web.Controllers
{
    public class AskController : BaseController
    {
        [Inject]
        public IAskService askService { set; get; }

        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            PagedList<TopicViewModel> list = askService.GetTopicListByPage("", pageIndex, pageSize);
            return View(list);
        }

        public IActionResult Show()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Publish()
        {
            ViewBag.Nodes = askService.GetAllNodes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Publish(Topic model)
        {
            model.UserId = CurrentUser.Id;
            askService.SaveTopic(model);
            if (askService.IsSuccess)
            {
                return Redirect("/Ask/Index");
            }
            return View();
        }
    }
}
