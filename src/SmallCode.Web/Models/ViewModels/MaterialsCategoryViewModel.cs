using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class MaterialsCategoryViewModel : BaseViewModel
    {

        public string Title { set; get; }

        public string Description { set; get; }

        /// <summary>
        /// 编程方向
        /// </summary>
        public Guid ProgrammingID { set; get; }

        public string Programming { set; get; }

        public MaterialsCategoryViewModel() { }

        public MaterialsCategoryViewModel(MaterialsCategory category)
        {
            this.Title = category.Title;
            this.Description = category.Description;
            this.ProgrammingID = category.ProgrammingID;
        }

    }
}
