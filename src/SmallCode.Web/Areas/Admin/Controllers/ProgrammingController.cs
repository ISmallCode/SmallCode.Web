using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Services;
using SmallCode.Web.Filters;
using SmallCode.Web.Models;
using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Web.DataModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class ProgrammingController : BaseController
    {
        [Inject]
        public IProgrammingService programmingService { set; get; }

        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<Programming> data = programmingService.GetListByPage(Title, PageIndex, PageSize);
            return View(data);
        }

        #region 修改
        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            ProgrammingViewModel model = null;
            if (Id.HasValue)
            {
                Programming programming = programmingService.GetProgrammingById(Id);
                model = new ProgrammingViewModel(programming);
                model.IsNew = false;
            }
            else
            {
                model = new ProgrammingViewModel();
                model.IsNew = true;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProgrammingViewModel model)
        {

            Programming programming = new Programming();
            programming.Id = model.Id;
            programming.Title = model.Title;
            programming.Description = model.Description;
            if (model.IsNew)
            {
                programming.CreateBy = CurrentUser.Id;
                programmingService.Save(programming);
            }
            else
            {
                programming.ModifyBy = CurrentUser.Id;
                programmingService.Update(programming);
            }

            if (programmingService.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", programmingService.ReturnMsg);
            }
            return View();
        }

        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult Delete(Guid id)
        {
            programmingService.Remove(id);
            ReturnResult<object> model = new ReturnResult<object>();
            model.Status = programmingService.IsSuccess ? "ok" :"error";
            model.ReturnMsg = programmingService.ReturnMsg;
            return Json(model);
        }
    }
}
