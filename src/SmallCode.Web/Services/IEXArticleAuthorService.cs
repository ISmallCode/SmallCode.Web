using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;

namespace SmallCode.Web.Services
{
    public interface IEXArticleAuthorService : IBaseService
    {
        PagedList<EXArticleAuthorViewModel> GetListByPage(string title, int pageIndex, int pageSize);
    }
}
