using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    /// <summary>
    /// 用来抓取文章的分类
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// 分类标题
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { set; get; }

        public virtual User CreateUser { set; get; }
    }
}
