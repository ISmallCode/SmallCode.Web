using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace SmallCode.Web.Models
{
    public class SCContext : DbContext
    {
        public SCContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<User> Users { set; get; }

        public DbSet<MaterialsCategory> MaterialsCategories { set; get; }

        public DbSet<Materials> Materialses { set; get; }

        public DbSet<Programming> Programmings { set; get; }

        public DbSet<EXArticleTemp> EXArticleTemps { set; get; }

        public DbSet<EXArticleAuthor> EXArticleAuthors { set; get; }

        public DbSet<EXArticle> EXArticles { set; get; }

        public DbSet<EXArticleCategory> EXArticleCategories { set; get; }

        public DbSet<ArticleCategory> ArticleCategories { set; get; }

        public DbSet<Article> Articles { set; get; }

        /// <summary>
        /// 互联网分类
        /// </summary>
        public DbSet<Category> Categories { set; get; }

        /// <summary>
        ///  互联网文章标签
        /// </summary>
        public DbSet<Label> Labels { set; get; }

        /// <summary>
        /// 文章的分类
        /// </summary>
        public DbSet<ArticleLabel> ArticleLabels { set; get; }

        /// <summary>
        /// 文章回复
        /// </summary>
        public DbSet<ArticleReply> ArticleReplies { set; get; }

        public DbSet<Topic> Topices { set; get; }

        public DbSet<TopicNode> TopicNodes { set; get; }

        public DbSet<TopicReply> TopicReplies { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("uuid-ossp");
            base.OnModelCreating(builder);
        }

    }
}
