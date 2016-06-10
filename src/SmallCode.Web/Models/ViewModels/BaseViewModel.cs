using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { set; get; }

        public DateTime CreateDate { set; get; }

        public bool IsDelete { set; get; }

        public bool IsNew { set; get; }

        public Guid CreateBy { set; get; }

        public string CreateUserName { set; get; }
    }
}
