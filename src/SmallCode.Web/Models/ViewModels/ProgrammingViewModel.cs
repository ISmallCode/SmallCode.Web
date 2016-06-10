using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class ProgrammingViewModel : BaseViewModel
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public ProgrammingViewModel() { }

        public ProgrammingViewModel(Programming model)
        {
            this.Title = model.Title;
            this.Description = model.Description;
        }
    }
}
