using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.DataModels
{
    public class JsonResult<T>
    {
        //状态  ok error
        public ResultStatus Status { set; get; }

        //消息
        public string Message { set; get; }

        //返回结果
        public T Result { set; get; }
    }

    public enum ResultStatus { OK, Error }
}
