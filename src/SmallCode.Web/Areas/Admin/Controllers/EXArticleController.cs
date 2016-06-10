using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Pager;

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class EXArticleController : BaseController
    {
        [Inject]
        public IEXArticleService exArticleService { set; get; }

        [HttpGet]
        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            base.Parameters["Title"] = Title;
            PagedList<EXArticleViewModel> data = exArticleService.GetListByPage(Title, PageIndex, PageSize);
            WebViewModel<EXArticleViewModel> model = new WebViewModel<EXArticleViewModel> { PagedList = data, Parameters = Parameters };
            return View(model);
        }

    }
}
