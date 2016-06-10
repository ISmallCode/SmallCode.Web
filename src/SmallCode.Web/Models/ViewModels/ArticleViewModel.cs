using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public int Browses { set; get; }

        public Guid CategoryId { set; get; }

        public string Category { set; get; }

        public string Summary { set; get; }

        public ArticleViewModel()
        {

        }

        public ArticleViewModel(Article model)
        {
            this.Title = model.Title;
            this.Description = model.Description;
            this.Browses = model.Browses;
            this.CategoryId = model.CategoryId;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.IsDelete = model.IsDelete;
            this.Category = model.Category.Description;
        }

    }
}
