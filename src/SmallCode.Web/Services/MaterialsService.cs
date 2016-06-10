using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Web.Models;
using SmallCode.Web.Filters;

namespace SmallCode.Web.Services
{
    public class MaterialsService : BaseService, IMaterialsService
    {

        private readonly SMContext db;

        public MaterialsService(SMContext _context)
        {
            db = _context;
        }

        /// <summary>
        /// 得到最新的10条
        /// </summary>
        /// <returns></returns>
        public List<Materials> GetLatest10()
        {
            return db.Materialses.OrderByDescending(x => x.CreateDate).Take(10).ToList();
        }


    }
}
