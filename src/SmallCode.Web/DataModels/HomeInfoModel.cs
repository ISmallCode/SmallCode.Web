using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.DataModels
{
    public class HomeInfoModel
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public string ImageUrl { set; get; }

        public DateTime CreateDate { set; get; }

        public string Category { set; get; }

        public int Browses { set; get; }

        public string Url { set; get; }
    }
}
