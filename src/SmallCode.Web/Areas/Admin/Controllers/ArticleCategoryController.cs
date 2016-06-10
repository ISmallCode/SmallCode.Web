using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Web.Models;
using SmallCode.Web.DataModels;
using SmallCode.Pager;

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class ArticleCategoryController : BaseController
    {
        [Inject]
        public IArticleCategoryService service { set; get; }

        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<ArticleCategory> data = service.GetListByPage(Title, PageIndex, PageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            ArticleCategoryViewModel model = null;
            if (Id.HasValue)
            {
                ArticleCategory category = service.GetCategoryById(Id);
                model = new ArticleCategoryViewModel(category);
                model.IsNew = false;
            }
            else
            {
                model = new ArticleCategoryViewModel();
                model.IsNew = true;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ArticleCategoryViewModel model)
        {
            if (model.IsNew)
            {
                ArticleCategory category = new ArticleCategory();
                category.Title = model.Title;
                category.Description = model.Description;
                category.CreateBy = CurrentUser.Id;
                service.Save(category);
            }
            else
            {
                ArticleCategory category = new ArticleCategory();
                category = service.GetCategoryById(model.Id);
                category.Title = model.Title;
                category.Description = model.Description;
                category.ModifyBy = CurrentUser.Id;
                service.Update(category);
            }

            if (service.IsSuccess)
            {
                return Redirect("/Admin/ArticleCategory/Index");
            }
            else
            {
                ModelState.AddModelError("", service.ReturnMsg);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            service.Remove(id);
            ReturnResult<object> model = new ReturnResult<object>();
            model.Status = service.IsSuccess ? "ok" : "error";
            model.ReturnMsg = service.ReturnMsg;
            return Json(model);
        }
    }
}
