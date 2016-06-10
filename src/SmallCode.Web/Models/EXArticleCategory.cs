using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class EXArticleCategory
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { set; get; }
    }
}
