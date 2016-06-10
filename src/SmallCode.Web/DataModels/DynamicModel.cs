using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.DataModels
{
    public class DynamicModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { set; get; }

        //地址
        public string Url { set; get; }

        //创建时间
        public DateTime CreateDate { set; get; }
    }
}
