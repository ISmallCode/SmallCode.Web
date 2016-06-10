using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class ArticleLabel : BaseEntity
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int ArticleId { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        public int Label { set; get; }

 
    }
}
