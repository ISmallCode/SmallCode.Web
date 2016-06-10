using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class EXArticleViewModel : BaseViewModel
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
        /// 分类Id
        /// </summary>
        public Guid CategoryId { set; get; }

        //分类
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
        /// 临时表id
        /// </summary>
        public int TempId { set; get; }

        /// <summary>
        /// 作者ID
        /// </summary>
        public Guid? AuthorId { set; get; }


        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { set; get; }

        public EXArticleViewModel()
        {

        }

        public EXArticleViewModel(EXArticle model)
        {
            this.Id = model.Id;
            this.AuthorId = model.AuthorId;
            this.Browses = model.Browses;
            this.CategoryId = model.CategoryId;
            this.Category = model.Category.Description;
            this.CreateBy = model.CreateBy;
            this.CreateDate = model.CreateDate;
            this.CreateUserName = model.CreateUser.UserName;
            this.Description = model.Description;
            this.IsDelete = model.IsDelete;
            this.Label = model.Label;
            this.OldBrowses = model.OldBrowses;
            this.ReplyCount = model.ReplyCount;
            this.Source = model.Source;
            this.TempId = model.TempId;
            this.Title = model.Title;
            this.URL = model.URL;
        }

    }
}
