 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class ArticleCategoryViewModel : BaseViewModel
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public ArticleCategoryViewModel() { }

        public ArticleCategoryViewModel(ArticleCategory model)
        {
            this.Id = model.Id;
            this.Title = model.Title;
            this.Description = model.Description;
        }
    }
}
