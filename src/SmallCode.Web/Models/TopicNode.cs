using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class TopicNode
    {
        public Guid Id { get; set; }


        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
