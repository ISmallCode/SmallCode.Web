using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class ArticleReply : BaseEntity
    {
        public int ArticleId { set; get; }

        public string Description { set; get; }

        public Guid? FatherId { set; get; }
 
    }
}
