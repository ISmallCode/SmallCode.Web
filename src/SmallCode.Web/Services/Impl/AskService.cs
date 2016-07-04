using SmallCode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;

namespace SmallCode.Web.Services.Impl
{
    public class AskService : BaseService, IAskService
    {
        private readonly SCContext db;

        public AskService(SCContext _context)
        {
            db = _context;
        }

        public PagedList<TopicViewModel> GetListByPage(string title, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

    }
}
