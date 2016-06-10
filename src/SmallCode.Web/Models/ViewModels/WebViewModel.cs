using SmallCode.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class WebViewModel<T>
    {
        public PagedList<T> PagedList { set; get; }

        public IDictionary<string, object> Parameters { set; get; }
    }
}
