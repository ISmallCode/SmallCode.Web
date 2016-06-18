using SmallCode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Services.Impl
{
    public class AskService : BaseService, IBaseService
    {
        private readonly SCContext db;

        public AskService(SCContext _context)
        {
            db = _context;
        }
    }
}
