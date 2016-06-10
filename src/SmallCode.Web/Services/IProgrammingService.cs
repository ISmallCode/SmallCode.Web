using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;

namespace SmallCode.Web.Services
{
    public interface IProgrammingService : IBaseService
    {
        PagedList<Programming> GetListByPage(string title, int pageIndex, int pageSize);
        Programming GetProgrammingById(Guid? id);
        void Save(Programming programming);
        void Update(Programming programming);
        void Remove(Guid id);
    }
}
