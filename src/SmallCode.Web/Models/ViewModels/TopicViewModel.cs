using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class TopicViewModel
    {
        public Guid Id { get; set; }

        public Guid NodeId { get; set; }

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
        /// 最后回复者名称
        /// </summary>
        public string LastReplyUserName { set; get; }

        /// <summary>
        /// 最后恢复时间
        /// </summary>
        public DateTime? LastReplyDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { set; get; }

        public TopicViewModel() { }

        public TopicViewModel(Topic model)
        {
            this.Id = model.Id;
            this.NodeId = model.NodeId;
            this.UserId = model.UserId;
            this.Title = model.Title;
            this.Description = model.Description;
            this.Top = model.Top;
            this.Good = model.Good;
            this.Browses = model.Browses;
            this.ReplyCount = model.ReplyCount;
            this.LastReplyUserId = model.LastReplyUserId;
            this.LastReplyDate = model.LastReplyDate;
            this.CreateDate = model.CreateDate;
            this.UserName = model.User.UserName;
            this.NodeName = model.TopicNode.Name;
        }
    }
}
