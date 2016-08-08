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
    public class EXArticleTempService : BaseService, IEXArticleTempService
    {

        private readonly SCContext db;

        public EXArticleTempService(SCContext _context)
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
        public PagedList<EXArticleTempViewModel> GetListByPage(string title, int pageIndex, int pageSize)
        {
            IQueryable<EXArticleTempViewModel> query =
                from ex in db.EXArticleTemps
                join u in db.Users on ex.CreateBy equals u.Id
                join c in db.Categories on ex.CategoryId equals c.Id
                select new EXArticleTempViewModel
                {
                    AuthorId = ex.AuthorId,
                    Category = c.Title,
                    Title = ex.Title,
                    CategoryId = c.Id,
                    CheckCategory = ex.CheckCategory,
                    CreateBy = ex.CreateBy,
                    CreateDate = ex.CreateDate,
                    CreateUserName = u.UserName,
                    Description = ex.Description,
                    FenCi = ex.FenCi,
                    Id = ex.Id,
                    IsCheck = ex.IsCheck,
                    IsDelete = ex.IsDelete,
                    Label = ex.Label,
                    OldBrowses = ex.OldBrowses,
                    ReplyCount = ex.ReplyCount,
                    Source = ex.Source,
                    Status = ex.Status,
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
        public EXArticleTemp GetArticleTempById(Guid id)
        {
            return db.EXArticleTemps.FirstOrDefault(x => x.Id == id);
        }
    }
}
