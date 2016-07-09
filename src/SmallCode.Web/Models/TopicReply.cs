using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class TopicReply
    {

        public Guid Id { get; set; }

        [ForeignKey("Topic")]
        public Guid TopicId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public Guid? ParentId { set; get; }

        public string ReplyEmail { get; set; }

        public string ReplyContent { get; set; }

        public DateTime CreateDate { get; set; }


        public virtual Topic Topic { set; get; }

        public virtual User User { set; get; }
    }
}
