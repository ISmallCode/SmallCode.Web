using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;
using SmallCode.Web.Models;
using SmallCode.Web.Models.ViewModels;

namespace SmallCode.Web.Services
{
    public interface IMaterialsService:IBaseService
    {
        List<Materials> GetLatest10();
        PagedList<MaterialsViewModel> GetListByPage(string title, int pageIndex, int pageSize);
        List<MaterialsCategory> GetAllMaterialsCategories();
        Materials GetMaterialsById(Guid? id);
        void Save(Materials materials);
        void Update(Materials materials);
        MaterialsCategory GetMaterialsCategoryById(Guid? id);
        PagedList<MaterialsCategoryViewModel> GetCategoryListByPage(string title, int pageIndex, int pageSize);
        List<Programming> GetAllProgrammings();
        void SaveCategory(MaterialsCategory category);
        void UpdateCategory(MaterialsCategory category);
        void RemoveCategory(Guid id);
        void Remove(Guid id);
    }
}
