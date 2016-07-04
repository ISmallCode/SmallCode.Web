using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Services
{
    public interface IAskService : IBaseService
    {
        PagedList<TopicViewModel> GetListByPage(string title, int pageIndex, int pageSize);
    }
}
