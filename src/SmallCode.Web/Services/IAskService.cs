using SmallCode.Pager;
using SmallCode.Web.Models;
using SmallCode.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Services
{
    public interface IAskService : IBaseService
    {
        PagedList<TopicViewModel> GetTopicListByPage(string title, int pageIndex, int pageSize);

        void SaveTopic(Topic model);

        List<TopicNode> GetAllNodes();
        Topic GetTopicById(Guid id);
        void SaveReply(TopicReply reply);
        void UpdateTopic(Topic topic);
        List<TopicReply> GetAllRepliesByTopicId(Guid id);

        List<Topic> GetHotTopices();
        PagedList<TopicViewModel> GetUserTopicListByPage(string v, int pageIndex, int pageSize,Guid Id);
    }
}
