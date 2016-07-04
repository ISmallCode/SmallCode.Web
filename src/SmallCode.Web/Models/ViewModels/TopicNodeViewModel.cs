using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class TopicNodeViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string NodeName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Order { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
