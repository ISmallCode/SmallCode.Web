using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Web.Models;

namespace SmallCode.Web.Services
{
    public interface IMaterialsService:IBaseService
    {
        List<Materials> GetLatest10();
    }
}
