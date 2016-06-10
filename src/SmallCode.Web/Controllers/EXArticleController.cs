using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Filters;
using SmallCode.Web.Services;
using SmallCode.Web.Models;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Pager;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallCode.Web.Controllers
{
    public class EXArticleController : Controller
    {

        [Inject]
        public IEXArticleService articleService { set; get; }

        // GET: /<controller>/
        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            PagedList<EXArticleViewModel> list = articleService.GetListByPage("", pageIndex, pageSize);
            return View(list);
        }


        /// <summary>
        /// 展示抓取的文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Show(Guid id)
        {
            EXArticle article = new EXArticle();
            article = articleService.GetArticleById(id);
            EXArticleViewModel _article = new EXArticleViewModel(article);
            return View(_article);
        }


    }
}
