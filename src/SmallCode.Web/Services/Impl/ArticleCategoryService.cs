using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace SmallCode.Web.Services.Impl
{
    public class ArticleCategoryService : BaseService, IArticleCategoryService
    {
        private readonly SCContext db;

        public ArticleCategoryService(SCContext _context)
        {
            db = _context;
        }

        public ArticleCategory GetCategoryById(Guid? id)
        {
            return db.ArticleCategories.AsNoTracking().AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public PagedList<ArticleCategory> GetListByPage(string title, int pageIndex, int pageSize)
        {
            IQueryable<ArticleCategory> query = db.ArticleCategories.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }
            return query.ToPagedList(pageIndex, pageSize);
        }

        public void Remove(Guid id)
        {
            bool flag = db.Articles.Where(x => x.CategoryId == id).Count() > 0;
            if (flag)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "该分类下有文章不能删除";
            }
            else
            {
                ArticleCategory category = db.ArticleCategories.FirstOrDefault(x => x.Id == id);
                db.ArticleCategories.Remove(category);
                bool result = db.SaveChanges() > 0;
                base.IsSuccess = result;
                base.ReturnMsg = result ? "删除成功" : "删除失败";
            }
        }

        public void Save(ArticleCategory model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            db.ArticleCategories.Add(model);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "保存成功" : "保存失败";
        }

        public void Update(ArticleCategory category)
        {
            ArticleCategory old = db.ArticleCategories.FirstOrDefault(x => x.Id == category.Id);
            old.Title = category.Title;
            old.Description = category.Description;
            old.ModifyBy = category.ModifyBy;
            old.ModifyDate = DateTime.Now;

            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "删除成功" : "删除失败";
        }

        /// <summary>
        /// 得到全部的分类
        /// </summary>
        public List<ArticleCategory> GetAllCategories()
        {
            return db.ArticleCategories.ToList();
        }
    }
}
