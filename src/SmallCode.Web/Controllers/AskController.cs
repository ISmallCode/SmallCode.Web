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

        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Show(Guid id)
        {
            Topic topic = askService.GetTopicById(id);
            topic.Browses++;
            askService.UpdateTopic(topic);
            ViewBag.Replies = askService.GetAllRepliesByTopicId(id);
            return View(new TopicViewModel(topic));
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

        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="TopicId"></param>
        /// <param name="ReplyEmail"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public IActionResult Reply(Guid TopicId, string ReplyEmail, string Description)
        {
            TopicReply reply = new TopicReply
            {
                CreateDate = DateTime.Now,
                ReplyContent = Description,
                ReplyEmail = ReplyEmail,
                TopicId = TopicId,
                UserId = CurrentUser.Id,
            };

            askService.SaveReply(reply);
            return Redirect("/Ask/Show/" + TopicId);
        }
    }
}
