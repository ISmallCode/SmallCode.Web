using SmallCode.Pager;
using SmallCode.Web.Extensions;
using SmallCode.Web.Models;
using SmallCode.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Services.Impl
{
    public class EXArticleService : BaseService, IEXArticleService
    {
        private readonly SCContext db;

        public EXArticleService(SCContext _context)
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
        public PagedList<EXArticleViewModel> GetListByPage(string title, int pageIndex, int pageSize)
        {

            IQueryable<EXArticleViewModel> query =
                from ex in db.EXArticles
                join u in db.Users on ex.CreateBy equals u.Id
                join c in db.Categories on ex.Id equals c.Id
                select new EXArticleViewModel
                {
                    AuthorId = ex.AuthorId,
                    CreateBy = ex.CreateBy,
                    Id = ex.Id,
                    Browses = ex.Browses,
                    Category = c.Title,
                    CategoryId = c.Id,
                    Title = ex.Title,
                    CreateDate = ex.CreateDate,
                    CreateUserName = u.UserName,
                    Description = ex.Description,
                    IsDelete = ex.IsDelete,
                    Label = ex.Label,
                    OldBrowses = ex.OldBrowses,
                    ReplyCount = ex.ReplyCount,
                    Source = ex.Source,
                    TempId = ex.TempId,
                    URL = ex.URL,
                    Summary = ex.Description.SubString(200, "......")
                };

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            return query.ToPagedList(pageIndex, pageSize);
        }


        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EXArticle GetArticleById(Guid id)
        {
            return db.EXArticles.FirstOrDefault(x => x.Id == id);
        }
    }
}
