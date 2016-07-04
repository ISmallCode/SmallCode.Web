using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.DataModels;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Pager;
using SmallCode.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class MaterialsController : BaseController
    {
        [Inject]
        public IMaterialsService materialsService { set; get; }

        public IActionResult Index(string Title = "", int PageIndex = 1, int PageSize = 20)
        {
            base.Parameters["Title"] = Title;
            PagedList<MaterialsViewModel> data = materialsService.GetListByPage(Title, PageIndex, PageSize);
            WebViewModel<MaterialsViewModel> model = new WebViewModel<MaterialsViewModel> { PagedList = data, Parameters = Parameters };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            List<MaterialsCategory> categories = materialsService.GetAllMaterialsCategories();
            ViewData["CategoryList"] = categories.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            //List<SelectListItem> CategoryList = new List<SelectListItem>();
            //CategoryList.Add(new SelectListItem { Value = "", Text = "分类" });
            //foreach (var item in categories)
            //{
            //    CategoryList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            //}
            //ViewData["CategoryList"] = CategoryList;


            //List<SelectListItem> MaterialsTypeList = new List<SelectListItem>();
            //MaterialsTypeList.Add(new SelectListItem { Value = "", Text = "资料类型" });
            //MaterialsTypeList.Add(new SelectListItem { Value = "0", Text = "视屏" });
            //MaterialsTypeList.Add(new SelectListItem { Value = "1", Text = "电子书" });
            //MaterialsTypeList.Add(new SelectListItem { Value = "2", Text = "文档资料" });
            //MaterialsTypeList.Add(new SelectListItem { Value = "3", Text = "博客" });
            //MaterialsTypeList.Add(new SelectListItem { Value = "4", Text = "项目" });
            //ViewData["MaterialsTypeList"] = MaterialsTypeList;

            //List<SelectListItem> SourceTypeList = new List<SelectListItem>();
            //SourceTypeList.Add(new SelectListItem { Value = "", Text = "来源类型" });
            //SourceTypeList.Add(new SelectListItem { Value = "0", Text = "原创" });
            //SourceTypeList.Add(new SelectListItem { Value = "1", Text = "转载" });
            //ViewData["SourceTypeList"] = SourceTypeList;

            MaterialsViewModel model = null;

            if (Id.HasValue)
            {
                Materials Materials = materialsService.GetMaterialsById(Id);
                model = new MaterialsViewModel(Materials);
                model.IsNew = false;
            }
            else
            {
                model = new MaterialsViewModel();
                model.IsNew = true;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MaterialsViewModel model)
        {

            Materials Materials = new Materials();
            Materials.Id = model.Id;
            Materials.MaterialsType = model.MaterialsType;
            Materials.Source = model.Source;
            Materials.SourceType = model.SourceType;
            Materials.Title = model.Title;
            Materials.CategoryId = model.CategoryId;
            Materials.Description = model.Description;
            if (model.IsNew)
            {
                Materials.CreateBy = CurrentUser.Id;
                materialsService.Save(Materials);
                DynamicModel dModel = new DynamicModel();
                dModel.CreateDate = Materials.CreateDate;
                dModel.Title = Materials.Title;
                dModel.Url = "/Materials/Show" + Materials.Id;
                dModel.Id = Materials.Id;

                ///将更新发到全部的客户端  这里用到了DI
                IConnectionManager conn = HttpContext.RequestServices.GetService<ConnectionManager>();
                IHubContext context = conn.GetHubContext<SMHub>();
                context.Clients.All.Send("info", dModel);

            }
            else
            {
                Materials.ModifyBy = CurrentUser.Id;
                materialsService.Update(Materials);
            }

            if (materialsService.IsSuccess)
            {
                return Redirect("/Admin/Materials/Index");
            }
            else
            {
                ModelState.AddModelError("", materialsService.ReturnMsg);
            }
            return View();
        }


        public IActionResult CategoryManage(string Title = "", int PageIndex = 1, int PageSize = 20)
        {
            base.Parameters["Title"] = Title;
            PagedList<MaterialsCategoryViewModel> data = materialsService.GetCategoryListByPage(Title, PageIndex, PageSize);
            WebViewModel<MaterialsCategoryViewModel> model = new WebViewModel<MaterialsCategoryViewModel> { PagedList = data, Parameters = Parameters };
            return View(model);
        }

        [HttpGet]
        public IActionResult CategoryEdit(Guid? Id)
        {
            MaterialsCategoryViewModel model = null;
            if (Id.HasValue)
            {
                MaterialsCategory category = materialsService.GetMaterialsCategoryById(Id);
                model = new MaterialsCategoryViewModel(category);
                model.IsNew = false;
            }
            else
            {
                model = new MaterialsCategoryViewModel();
                model.IsNew = true;
            }
            List<Programming> programmings = materialsService.GetAllProgrammings();
            List<SelectListItem> ProgrammingList = new List<SelectListItem>();
            ProgrammingList.Add(new SelectListItem { Value = "", Text = "选择编程" });
            foreach (var item in programmings)
            {
                ProgrammingList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            }
            ViewData["ProgrammingList"] = ProgrammingList;
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(MaterialsCategoryViewModel model)
        {
            List<Programming> programmings = materialsService.GetAllProgrammings();
            List<SelectListItem> ProgrammingList = new List<SelectListItem>();
            ProgrammingList.Add(new SelectListItem { Value = "", Text = "选择编程" });
            foreach (var item in programmings)
            {
                ProgrammingList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            }
            ViewData["ProgrammingList"] = ProgrammingList;


            MaterialsCategory category = new MaterialsCategory();
            category.Id = model.Id;
            category.Description = model.Description;
            category.ProgrammingID = model.ProgrammingID;
            category.Title = model.Title;
            if (model.IsNew)
            {
                category.CreateBy = CurrentUser.Id;
                materialsService.SaveCategory(category);
            }
            else
            {
                category.ModifyBy = CurrentUser.Id;
                materialsService.UpdateCategory(category);
            }

            if (materialsService.IsSuccess)
            {
                return Redirect("/Admin/Materials/CategoryManage");
            }
            else
            {
                ModelState.AddModelError("", materialsService.ReturnMsg);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteCategory(Guid id)
        {
            materialsService.RemoveCategory(id);
            ReturnResult<object> model = new ReturnResult<object>();
            model.Status = materialsService.IsSuccess ? "success" : "fail";
            model.ReturnMsg = materialsService.ReturnMsg;
            return Json(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            materialsService.Remove(id);
            ReturnResult<object> model = new ReturnResult<object>();
            model.Status = materialsService.IsSuccess ? "success" : "fail";
            model.ReturnMsg = materialsService.ReturnMsg;
            return Json(model);
        }

    }
}
