using SmallCode.Pager;
using SmallCode.Web.Models;
using SmallCode.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Services.Impl
{
    public class EXArticleAuthorService : BaseService, IEXArticleAuthorService
    {

        private readonly SMContext db;

        public EXArticleAuthorService(SMContext _context)
        {
            db = _context;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<EXArticleAuthorViewModel> GetListByPage(string userName, int pageIndex, int pageSize)
        {

            IQueryable<EXArticleAuthorViewModel> query =
                from ex in db.EXArticleAuthors
                join u in db.Users on ex.CreateBy equals u.Id
                select new EXArticleAuthorViewModel
                {
                    CreateBy = ex.CreateBy,
                    Id = ex.Id,
                    CreateDate = ex.CreateDate,
                    CreateUserName = u.UserName,
                    UserName = ex.UserName,
                    Description = ex.Description,
                    Email = ex.Email,
                    Fans = ex.Fans,
                    Fellow = ex.Fellow,
                    Field = ex.Field,
                    Info = ex.Info,
                    IsDelete = ex.IsDelete,
                    NickName = ex.NickName,
                    Phone = ex.Phone,
                    QQ = ex.QQ,
                    Score = ex.Score,
                    Skill = ex.Skill,
                    Source = ex.Source,
                    UserInfoUrl = ex.UserInfoUrl,
                    UserWordUrl = ex.UserWordUrl,
                    WeiXin = ex.WeiXin
                };

            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName.Contains(userName));
            }

            return query.ToPagedList(pageIndex, pageSize);
        }
    }
}

