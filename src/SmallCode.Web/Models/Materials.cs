using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    /// <summary>
    /// 资料
    /// </summary>
    public class Materials : BaseEntity
    {
        public string Title { set; get; }

        public string Description { set; get; }

        /// <summary>
        /// 资料类型
        /// </summary>
        public MaterialsType MaterialsType { set; get; }

        /// <summary>
        /// 资料分类
        /// </summary>
        public Guid CategoryId { set; get; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { set; get; }

        /// <summary>
        /// 来源类型
        /// </summary>
        public SourceType SourceType { set; get; }


        /// <summary>
        /// 浏览量
        /// </summary>
        public int Browses { set; get; }
    }
}
