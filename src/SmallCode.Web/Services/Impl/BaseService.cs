using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Services.Impl
{
    public class BaseService : IBaseService
    {
        public bool IsNew
        {
            get; set;
        }

        public bool IsSuccess
        {
            get; set;
        }

        public int PageIndex
        {
            set; get;
        }

        public int PageSize
        {
            set; get;
        }

        public string ReturnMsg
        {
            set; get;
        }

        public int TotalRecords
        {
            set; get;
        }
    }
}
