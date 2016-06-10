using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;

namespace SmallCode.Web.Services
{
    public interface IArticleCategoryService : IBaseService
    {
        ArticleCategory GetCategoryById(Guid? id);
        void Save(ArticleCategory category);
        void Update(ArticleCategory category);
        void Remove(Guid id);
        PagedList<ArticleCategory> GetListByPage(string title, int pageIndex, int pageSize);

        List<ArticleCategory> GetAllCategories();
    }
}
