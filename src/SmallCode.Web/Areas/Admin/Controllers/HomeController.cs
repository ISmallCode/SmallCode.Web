using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Web.Filters;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Net.Http.Headers;
using SmallCode.Web.Helpers;
using Microsoft.AspNetCore.Http;

namespace SmallCode.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImageUpload(IFormFile file, [FromServices]IHostingEnvironment env)
        {
            string root = @"images/article/";
            string ext = Path.GetExtension(file.FileName.Replace("\"", ""));
            var originalName = DateHelper.GetTimeStamp() + ext;
            var fileName = Path.Combine(root, originalName);
            string phyPath = Path.Combine(env.WebRootPath, fileName);

            if (!Directory.Exists(Path.Combine(env.WebRootPath, root)))
            {
                Directory.CreateDirectory(Path.Combine(env.WebRootPath, root));
            }
            if (file != null)
            {
                using (var stream = new FileStream(phyPath, FileMode.CreateNew))
                {
                    file.CopyTo(stream);
                }
                return Content("/" + root + originalName);
            }
            else
            {
                ModelState.AddModelError("", "没有选择文件");
                return Content("error");
            }
        }
    }
}
