using SmallCode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SmallCode.Web.Services.Impl
{
    public class AskService : BaseService, IAskService
    {
        private readonly SCContext db;

        public AskService(SCContext _context)
        {
            db = _context;
        }

        /// <summary>
        /// 得到全部节点
        /// </summary>
        /// <returns></returns>
        public List<TopicNode> GetAllNodes()
        {
            return db.TopicNodes.ToList();
        }

        /// <summary>
        /// 全部的回复根据主题的ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TopicReply> GetAllRepliesByTopicId(Guid id)
        {
            return db.TopicReplies.Where(x => x.TopicId == id).OrderBy(x => x.CreateDate).ToList();
        }

        /// <summary>
        /// 获取热门帖子
        /// </summary>
        /// <returns></returns>
        public List<Topic> GetHotTopices()
        {
            return db.Topices.OrderByDescending(x => x.ReplyCount).Take(8).ToList();
        }

        /// <summary>
        /// 根据Id找主题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Topic GetTopicById(Guid id)
        {
            return db.Topices.Where(x => x.Id == id).Include(x => x.User).Include(x => x.TopicNode).FirstOrDefault();
        }

        /// <summary>
        /// 分页查找主题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<TopicViewModel> GetTopicListByPage(string title, int pageIndex, int pageSize)
        {
            IQueryable<TopicViewModel> query =
                from a in db.Topices
                join n in db.TopicNodes on a.NodeId equals n.Id
                join u in db.Users on a.UserId equals u.Id
                join lu in db.Users on a.LastReplyUserId equals lu.Id into userTemp
                from ur in userTemp.DefaultIfEmpty()
                select new TopicViewModel
                {
                    Browses = a.Browses,
                    Title = a.Title,
                    Id = a.Id,
                    CreateDate = a.CreateDate,
                    Description = a.Description,
                    UserId = a.UserId,
                    Good = a.Good,
                    NodeId = a.NodeId,
                    LastReplyDate = a.LastReplyDate,
                    LastReplyUserId = a.LastReplyUserId,
                    LastReplyUserName = ur == null ? null : ur.UserName,
                    UserName = u.UserName,
                    NodeName = n.Name,
                    ReplyCount = a.ReplyCount,
                    Top = a.Top
                };

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            query = query.OrderByDescending(x => x.CreateDate);

            return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 得到用户的主题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public PagedList<TopicViewModel> GetUserTopicListByPage(string title, int pageIndex, int pageSize,Guid UserId)
        {
            IQueryable<TopicViewModel> query =
               from a in db.Topices
               join n in db.TopicNodes on a.NodeId equals n.Id
               join u in db.Users on a.UserId equals u.Id
               join lu in db.Users on a.LastReplyUserId equals lu.Id into userTemp
               from ur in userTemp.DefaultIfEmpty()
               where a.UserId==UserId
               select new TopicViewModel
               {
                   Browses = a.Browses,
                   Title = a.Title,
                   Id = a.Id,
                   CreateDate = a.CreateDate,
                   Description = a.Description,
                   UserId = a.UserId,
                   Good = a.Good,
                   NodeId = a.NodeId,
                   LastReplyDate = a.LastReplyDate,
                   LastReplyUserId = a.LastReplyUserId,
                   LastReplyUserName = ur == null ? null : ur.UserName,
                   UserName = u.UserName,
                   NodeName = n.Name,
                   ReplyCount = a.ReplyCount,
                   Top = a.Top
               };

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            query = query.OrderByDescending(x => x.CreateDate);

            return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 保存主题
        /// </summary>
        /// <param name="reply"></param>
        public void SaveReply(TopicReply reply)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    db.TopicReplies.Add(reply);

                    var topic = db.Topices.FirstOrDefault(x => x.Id == reply.TopicId);

                    topic.LastReplyDate = DateTime.Now;
                    topic.LastReplyUserId = reply.UserId;
                    topic.ReplyCount = topic.ReplyCount + 1;

                    db.SaveChanges();
                    trans.Commit();
                    base.IsSuccess = true;
                    base.ReturnMsg = "保存成功";
                }
                catch (Exception ex)
                {
                    base.IsSuccess = true;
                    base.ReturnMsg = "保存成功";
                }
            }
        }

        /// <summary>
        /// 保存主题
        /// </summary>
        /// <param name="model"></param>
        public void SaveTopic(Topic model)
        {
            model.CreateDate = DateTime.Now;
            model.Browses = 0;
            model.Good = false;
            model.Top = 0;

            db.Topices.Add(model);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "保存成功" : "保存失败";
        }

        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="topic"></param>
        public void UpdateTopic(Topic topic)
        {
            var old = db.Topices.FirstOrDefault(x => x.Id == topic.Id);
            old.Browses = topic.Browses;
            old.Description = topic.Description;
            old.Title = topic.Title;
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "修改成功" : "修改失败";
        }
    }
}
