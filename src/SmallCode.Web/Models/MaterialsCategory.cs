using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class MaterialsCategory : BaseEntity
    {
        public string Title { set; get; }

        public string Description { set; get; }

        /// <summary>
        /// 编程方向
        /// </summary>
        [ForeignKey("Programming")]
        public Guid ProgrammingID { set; get; }

        public virtual Programming Programming { set; get; }
    }
}
