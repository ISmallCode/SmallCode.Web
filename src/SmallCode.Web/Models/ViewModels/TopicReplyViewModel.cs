using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class TopicReplyViewModel
    {
        public Guid Id { get; set; }

        public Guid TopicId { get; set; }


        public Guid UserId { get; set; }

        public Guid? ParentId { set; get; }

        public string ReplyEmail { get; set; }

        public string ReplyContent { get; set; }

        public DateTime CreateDate { get; set; }

        public string TopicName { set; get; }

        public string UserName { set; get; }


    }
}
