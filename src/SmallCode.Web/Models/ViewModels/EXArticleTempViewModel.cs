using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class EXArticleTempViewModel : BaseViewModel
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
        /// 分类
        /// </summary>
        public Guid CategoryId { set; get; }


        /// <summary>
        /// 分类
        /// </summary>
        public string Category { set; get; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Label { set; get; }

        /// <summary>
        /// 原来文章评论数量
        /// </summary>
        public int? ReplyCount { set; get; }


        /// <summary>
        /// 分词
        /// </summary>
        public string FenCi { set; get; }

        public int? AuthorId { set; get; }


        /// <summary>
        /// 是否文章分类过
        /// </summary>
        public int IsCheck { set; get; }


        /// <summary>
        /// 文章
        /// </summary>
        public int CheckCategory { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public EXArticleTempStatus Status
        {
            set; get;
        }


        public EXArticleTempViewModel() { }

    }
}
