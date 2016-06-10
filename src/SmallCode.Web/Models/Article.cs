using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article : BaseEntity
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public int Browses { set; get; }

        /// <summary>
        ///  加ForeignKey 在查询的时候 加Include可以往 Category加导航属性
        /// </summary>
        [ForeignKey("Category")]
        public Guid CategoryId { set; get; }

        public virtual ArticleCategory Category { set; get; }
    }
}
