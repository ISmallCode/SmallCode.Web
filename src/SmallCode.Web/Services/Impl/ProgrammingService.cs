using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace SmallCode.Web.Services.Impl
{
    public class ProgrammingService : BaseService, IProgrammingService
    {
        private readonly SCContext db;

        public ProgrammingService(SCContext _context)
        {
            db = _context;
        }

        public PagedList<Programming> GetListByPage(string title, int pageIndex, int pageSize)
        {

            IQueryable<Programming> query = db.Programmings.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }
            return query.ToPagedList(pageIndex, pageSize);

        }

        public Programming GetProgrammingById(Guid? id)
        {
            return db.Programmings.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            Programming programming = db.Programmings.FirstOrDefault(x => x.Id == id);
            db.Programmings.Remove(programming);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "删除成功" : "删除失败";
        }

        public void Save(Programming model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            db.Programmings.Add(model);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "保存成功" : "保存失败";
        }

        public void Update(Programming programming)
        {
            Programming old = db.Programmings.Where(x => x.Id == programming.Id).FirstOrDefault();
            old.Title = programming.Title;
            old.Description = programming.Description;
            old.ModifyBy = programming.ModifyBy;
            old.ModifyDate = DateTime.Now;
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "保存成功" : "保存失败";
        }
    }
}
