using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Web.Models;
using SmallCode.Web.Filters;
using SmallCode.Web.Extensions;
using SmallCode.Pager;
using Microsoft.EntityFrameworkCore;

namespace SmallCode.Web.Services.Impl
{
    public class UserService : BaseService, IUserService
    {

        private readonly SCContext db;

        public UserService(SCContext _context)
        {
            db = _context;
        }

        /// <summary>
        /// 得到最新的10条用户
        /// </summary>
        /// <returns></returns>
        public List<User> GetLatest10()
        {
            return db.Users.OrderByDescending(x => x.CreatedDate).Take(10).ToList();
        }

        public User GetUserById(Guid id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList<User> GetUserByPage(string userName, int pageIndex, int pageSize)
        {
            IQueryable<User> query = db.Users.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName.Contains(userName));
            }
            return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string username, string password)
        {
            User user = new User();
            password = password.ToMD5Hash();
            user = db.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (user != null)
            {
                bool result = user.Password.Equals(password);
                if (result)
                {
                    base.IsSuccess = true;
                    base.ReturnMsg = "登录成功";
                }
                else
                {
                    user = null;
                    base.IsSuccess = false;
                    base.ReturnMsg = "密码错误";
                }
            }
            else
            {
                base.IsSuccess = false;
                base.ReturnMsg = "用户名错误";
            }
            return user;
        }

        /// <summary>
        ///  删除用户
        /// </summary>
        /// <param name="id"></param>
        public void Remove(Guid id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            db.Users.Remove(user);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "删除成功" : "删除失败";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Save(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.Id = Guid.NewGuid();
            user.Password = user.Password.ToMD5Hash();
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }



    }
}
