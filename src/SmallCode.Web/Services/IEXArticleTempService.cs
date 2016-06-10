using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;

namespace SmallCode.Web.Services
{
    public interface IEXArticleTempService : IBaseService
    {
        PagedList<EXArticleTempViewModel> GetListByPage(string title, int pageIndex, int pageSize);
    }
}
