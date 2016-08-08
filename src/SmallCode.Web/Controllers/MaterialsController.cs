using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Web.Services.Impl;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Pager;
using SmallCode.Web.Models;

namespace SmallCode.Web.Controllers
{
    public class MaterialsController : BaseController
    {
        [Inject]
        public IMaterialsService materialsService { set; get; }

        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            PagedList<MaterialsViewModel> list = materialsService.GetListByPage("", pageIndex, pageSize);
            return View(list);
        }

        [HttpGet]
        public IActionResult Show(Guid id)
        {
            Materials materials = new Materials();
            materials = materialsService.GetMaterialsById(id);
            materials.Browses++;
            materialsService.Update(materials);
            MaterialsViewModel model = new MaterialsViewModel(materials);
            model.Id = id;
            return View(model);
        }

    }
}
