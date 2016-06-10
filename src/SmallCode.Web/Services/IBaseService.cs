using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Services
{
    public interface IBaseService
    {
        int TotalRecords { set; get; }

        int PageSize { set; get; }

        int PageIndex { set; get; }

        bool IsNew { set; get; }// = true;

        bool IsSuccess { set; get; }// = true;

        string ReturnMsg { set; get; } //= "";
    }
}
