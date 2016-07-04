using SmallCode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models.ViewModels;

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
                    LastReplyUserName =ur==null?null:ur.UserName,
                    UserName = u.UserName,
                    NodeName = n.Name,
                    ReplyCount = a.ReplyCount,
                    Top = a.Top
                };

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            query = query.OrderByDescending(x=>x.CreateDate);

            return query.ToPagedList(pageIndex, pageSize);
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
    }
}
