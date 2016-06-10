using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models;
using SmallCode.Web.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        [Inject]
        public IArticleService articleService { set; get; }

        [Inject]
        public IArticleCategoryService articleCategoryService { set; get; }

        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<ArticleViewModel> data = articleService.GetListByPage(Title, PageIndex, PageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            ArticleViewModel model = null;
            if (Id.HasValue)
            {
                Article article = articleService.GetArticleById(Id);
                model = new ArticleViewModel(article);
                model.IsNew = false;
            }
            else
            {
                model = new ArticleViewModel();
                model.IsNew = true;
            }
            List<ArticleCategory> categories = articleCategoryService.GetAllCategories();

            List<SelectListItem> CategoryList = new List<SelectListItem>();
            CategoryList.Add(new SelectListItem { Value = "", Text = "选择分类" });
            foreach (var item in categories)
            {
                CategoryList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            }
            ViewData["CategoryList"] = CategoryList;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ArticleViewModel model)
        {

            List<ArticleCategory> categories = articleCategoryService.GetAllCategories();

            List<SelectListItem> CategoryList = new List<SelectListItem>();
            CategoryList.Add(new SelectListItem { Value = "", Text = "选择分类" });
            foreach (var item in categories)
            {
                CategoryList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            }
            ViewData["CategoryList"] = CategoryList;


            Article article = new Article();
            article.Id = model.Id;
            article.Title = model.Title;
            article.Description = model.Description;
            article.CategoryId = model.CategoryId;

            if (model.IsNew)
            {
                article.CreateBy = CurrentUser.Id;
                articleService.Save(article);
            }
            else
            {
                article.ModifyBy = CurrentUser.Id;
                articleService.Update(article);
            }


            if (articleService.IsSuccess)
            {
                return Redirect("/Admin/Article/Index");
            }
            else
            {
                ModelState.AddModelError("", articleService.ReturnMsg);
                return View(model);
            }
        }


        public IActionResult Show(Guid id)
        {
            Article article = articleService.GetArticleById(id);
            article.Description = CommonMark.CommonMarkConverter.Convert(article.Description);
            return View(new ArticleViewModel(article));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            articleService.Remove(id);
            ReturnResult<object> model = new ReturnResult<object>();
            model.Status = articleService.IsSuccess ? "ok" : "error";
            model.ReturnMsg = articleService.ReturnMsg;
            return Json(model);
        }

    }
}
