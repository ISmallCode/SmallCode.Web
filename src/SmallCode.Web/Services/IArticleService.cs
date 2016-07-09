using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;
using SmallCode.Web.Models.ViewModels;

namespace SmallCode.Web.Services
{
    public interface IArticleService : IBaseService
    {
        Article GetArticleById(Guid? id);
        PagedList<ArticleViewModel> GetListByPage(string title, int pageIndex, int pageSize);
        void Save(Article article);
        void Update(Article article);
        void Remove(Guid id);
        List<Article> GetLatest10();

        List<Article> GetHotArticles(int count = 8);
        List<Article> GetLastestArticles(int count = 8);
    }
}
