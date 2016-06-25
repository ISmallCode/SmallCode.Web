using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;

namespace SmallCode.Web.Services
{
    public interface IUserService:IBaseService
    {
        User Login(string username, string password);
        Task Save(User user);
        List<User> GetLatest10();
        PagedList<User> GetUserByPage(string userName, int pageIndex, int pageSize);
        void Remove(Guid id);
    }
}
