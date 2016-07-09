using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace SmallCode.Web.Services.Impl
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly SCContext db;

        public ArticleService(SCContext _context)
        {
            db = _context;
        }

        public Article GetArticleById(Guid? id)
        {
            // return db.Articles.Where(x => x.Id == id).FirstOrDefault();
            return db.Articles.Where(x => x.Id == id).Include(x=>x.CreateUser).Include(x => x.Category).FirstOrDefault();
        }

        /// <summary>
        /// 获取热门文章
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Article> GetHotArticles(int count = 8)
        {
            return db.Articles.OrderByDescending(x => x.Browses).Take(count).ToList();
        }

        /// <summary>
        /// 得到最新的
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Article> GetLastestArticles(int count = 8)
        {
            return db.Articles.OrderByDescending(x => x.CreateDate).Take(count).ToList();
        }

        public List<Article> GetLatest10()
        {
            return db.Articles.OrderByDescending(x => x.CreateDate).Take(10).ToList();
        }

        public PagedList<ArticleViewModel> GetListByPage(string title, int pageIndex, int pageSize)
        {
            IQueryable<ArticleViewModel> query =
                    from a in db.Articles
                    join c in db.ArticleCategories on a.CategoryId equals c.Id
                    join u in db.Users on a.CreateBy equals u.Id
                    select new ArticleViewModel
                    {
                        CreateBy = u.Id,
                        Browses = a.Browses,
                        Category = c.Title,
                        Title = a.Title,
                        Id = a.Id,
                        CategoryId = a.CategoryId,
                        CreateDate = a.CreateDate,
                        CreateUserName = u.UserName,
                        Description = a.Description,
                        IsDelete = a.IsDelete,
                        Summary = a.Description.SubString(200, "......")
                    };

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            return query.ToPagedList(pageIndex, pageSize);
        }

        public void Remove(Guid id)
        {
            Article article = db.Articles.FirstOrDefault(x => x.Id == id);
            db.Articles.Remove(article);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "删除成功" : "删除失败";
        }

        public void Save(Article model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            db.Articles.Add(model);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "保存成功" : "保存失败";
        }

        public void Update(Article article)
        {
            Article old = db.Articles.FirstOrDefault(x => x.Id == article.Id);
            old.Title = article.Title;
            old.Description = article.Description;
            old.ModifyBy = article.ModifyBy;
            old.CategoryId = article.CategoryId;
            old.ModifyDate = DateTime.Now;

            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "删除成功" : "删除失败";
        }
    }
}
