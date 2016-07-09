using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class Topic
    {
        public Guid Id { get; set; }

        [ForeignKey("TopicNode")]
        public Guid NodeId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// 置顶权重
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 精华
        /// </summary>
        public bool Good { get; set; }

        /// <summary>
        /// 查看次数
        /// </summary>
        public int Browses { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 最后回复者ID
        /// </summary>
        public Guid LastReplyUserId { get; set; }

        /// <summary>
        /// 最后恢复时间
        /// </summary>
        public DateTime? LastReplyDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }


        public virtual User User { set; get; }

        public virtual TopicNode TopicNode { set; get; }
    }
}
