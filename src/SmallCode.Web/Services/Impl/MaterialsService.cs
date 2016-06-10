using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Web.Models;
using SmallCode.Web.Filters;
using SmallCode.Web.Models.ViewModels;
using SmallCode.Pager;
using Microsoft.EntityFrameworkCore;
using SmallCode.Web.Extensions;

namespace SmallCode.Web.Services.Impl
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


        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList<MaterialsViewModel> GetListByPage(string title, int pageIndex, int pageSize)
        {

            IQueryable<MaterialsViewModel> query =
              (from s in db.Materialses
               join t in db.MaterialsCategories on s.CategoryId equals t.Id
               join u in db.Users on s.CreateBy equals u.Id
               orderby s.CreateDate descending
               select new MaterialsViewModel
               {
                   Id = s.Id,
                   Title = s.Title,
                   Description = s.Description,
                   Category = t.Title,
                   CategoryId = s.CategoryId,
                   CreateDate = s.CreateDate,
                   MaterialsType = s.MaterialsType,
                   Source = s.Source,
                   CreateBy = s.CreateBy,
                   SourceType = s.SourceType,
                   CreateUserName = u.UserName,
                   Browses = s.Browses,
                   Summary = s.Description.SubString(100, ".......")
               }).AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }
            return query.ToPagedList(pageIndex, pageSize);
        }


        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        public List<MaterialsCategory> GetAllMaterialsCategories()
        {
            return db.MaterialsCategories.ToList();
        }

        /// <summary>
        /// 保存资料
        /// </summary>
        /// <param name="model"></param>
        public void Save(Materials model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            db.Materialses.Add(model);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "保存成功" : "保存失败";
        }

        /// <summary>
        /// 根据ID得到资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Materials GetMaterialsById(Guid? id)
        {
            return db.Materialses.Where(x => x.Id == id).FirstOrDefault();
        }

        public PagedList<MaterialsCategoryViewModel> GetCategoryListByPage(string title, int pageIndex, int pageSize)
        {

            IQueryable<MaterialsCategoryViewModel> query =
               (from m in db.MaterialsCategories
                join p in db.Programmings on m.ProgrammingID equals p.Id
                join u in db.Users on m.CreateBy equals u.Id
                orderby m.CreateDate descending
                select new MaterialsCategoryViewModel
                {
                    Id = m.Id,
                    CreateDate = m.CreateDate,
                    CreateBy = m.CreateBy,
                    CreateUserName = u.UserName,
                    Description = m.Description,
                    IsDelete = m.IsDelete,
                    Programming = p.Title,
                    Title = m.Title,
                    ProgrammingID = p.Id
                }
                ).AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }
            return query.ToPagedList(pageIndex, pageSize);

        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="materials"></param>
        public void Update(Materials materials)
        {
            Materials old = db.Materialses.Where(x => x.Id == materials.Id).FirstOrDefault();

            old.CategoryId = materials.CategoryId;
            old.Description = materials.Description;
            old.MaterialsType = materials.MaterialsType;
            old.Source = materials.Source;
            old.SourceType = materials.SourceType;
            old.Title = materials.Title;
            old.Browses = materials.Browses;

            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "修改成功" : "修改失败";

        }

        /// <summary>
        /// 根据ID获取资料分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaterialsCategory GetMaterialsCategoryById(Guid? id)
        {
            return db.MaterialsCategories.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 得到全部编程
        /// </summary>
        public List<Programming> GetAllProgrammings()
        {
            return db.Programmings.ToList();
        }

        public void SaveCategory(MaterialsCategory model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            db.MaterialsCategories.Add(model);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "保存成功" : "保存失败";
        }

        public IQueryable<MaterialsViewModel> GetTest()
        {

            return (from s in db.Materialses
                    join t in db.MaterialsCategories on s.CategoryId equals t.Id
                    join u in db.Users on s.CreateBy equals u.Id
                    select new MaterialsViewModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        Category = t.Title,
                        CategoryId = s.CategoryId,
                        CreateDate = s.CreateDate,
                        MaterialsType = s.MaterialsType,
                        Source = s.Source,
                        CreateBy = s.CreateBy,
                        SourceType = s.SourceType,
                        CreateUserName = u.UserName
                    });

        }

        public void Remove(Guid id)
        {
            Materials Materials = db.Materialses.FirstOrDefault(x => x.Id == id);
            db.Materialses.Remove(Materials);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "删除成功" : "删除失败";
        }

        public void RemoveCategory(Guid id)
        {
            MaterialsCategory category = db.MaterialsCategories.FirstOrDefault(x => x.Id == id);
            db.MaterialsCategories.Remove(category);
            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "删除成功" : "删除失败";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void UpdateCategory(MaterialsCategory category)
        {
            MaterialsCategory old = db.MaterialsCategories.Where(x => x.Id == category.Id).FirstOrDefault();

            old.Title = category.Title;
            old.Description = category.Description;
            old.ModifyBy = category.ModifyBy;
            old.ModifyDate = category.ModifyDate;
            old.ProgrammingID = category.ProgrammingID;

            bool result = db.SaveChanges() > 0;
            base.IsSuccess = result;
            base.ReturnMsg = result ? "修改成功" : "修改失败";
        }

    }
}
