using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Filters;
using SmallCode.Web.Services;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Pager;
using SmallCode.Web.Models;

namespace SmallCode.Web.Controllers
{
    public class ArticleController : BaseController
    {

        [Inject]
        public IArticleService articleService { set; get; }

        // GET: /<controller>/
        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            PagedList<ArticleViewModel> list = articleService.GetListByPage("", pageIndex, pageSize);
            return View(list);
        }

        /// <summary>
        /// 文章展示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Show(Guid id)
        {
            Article article = new Article();
            article = articleService.GetArticleById(id);
            article.Browses++;
            articleService.Update(article);
            ArticleViewModel _article = new ArticleViewModel(article);
            return View(_article);
        }
    }
}
