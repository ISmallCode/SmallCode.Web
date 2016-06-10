using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid Id { set; get; }


        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { set; get; }

        /// <summary>
        /// 创建者
        /// </summary>
        public Guid CreateBy { set; get; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { set; get; }

        /// <summary>
        /// 修改者
        /// </summary>
        public Guid? ModifyBy { set; get; }

        /// <summary>
        ///  修改日期
        /// </summary>
        public DateTime? ModifyDate { set; get; }
    }
}
