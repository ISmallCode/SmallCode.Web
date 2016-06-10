using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    /// <summary>
    /// 文章的分类
    /// </summary>
    public class ArticleCategory : BaseEntity
    {
        public string Title { set; get; }

        public string Description { set; get; }
 
    }
}
