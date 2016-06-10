using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Pager;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class EXArticleAuthorController : BaseController
    {
        [Inject]
        public IEXArticleAuthorService exArticleAuthorService { set; get; }

        [HttpGet]
        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            base.Parameters["Title"] = Title;
            PagedList<EXArticleAuthorViewModel> data = exArticleAuthorService.GetListByPage(Title, PageIndex, PageSize);
            WebViewModel<EXArticleAuthorViewModel> model = new WebViewModel<EXArticleAuthorViewModel> { PagedList = data, Parameters = Parameters };
            return View(model);
        }
    }
}
