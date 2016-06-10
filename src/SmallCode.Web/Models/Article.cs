using System;
using System.Collections.Generic;
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

        public Guid CategoryId { set; get; }

        public virtual User User { set; get; }
    }
}
