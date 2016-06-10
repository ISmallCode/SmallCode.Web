using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    /// <summary>
    /// 博客
    /// </summary>
    public class EXArticle : BaseEntity
    {
        /// <summary>
        ///  标题
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 来源 网站站点
        /// </summary>
        public string Source { set; get; }

        /// <summary>
        ///  原来的博客的url
        /// </summary>
        public string URL { set; get; }

        /// <summary>
        /// 原来浏览量
        /// </summary>
        public int OldBrowses { set; get; }

        /// <summary>
        /// SmallCode 浏览量 
        /// </summary>
        public int Browses { set; get; }

        /// <summary>
        /// 分类
        /// </summary>
        public Guid CategoryId { set; get; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Label { set; get; }

        /// <summary>
        /// 原来文章评论数量
        /// </summary>
        public int? ReplyCount { set; get; }

        /// <summary>
        /// 临时表id
        /// </summary>
        public int TempId { set; get; }

        /// <summary>
        /// 作者ID
        /// </summary>
        public int? AuthorId { set; get; }
    }
}
