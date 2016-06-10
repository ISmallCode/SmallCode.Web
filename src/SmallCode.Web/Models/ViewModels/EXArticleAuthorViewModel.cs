using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models.ViewModels
{
    public class EXArticleAuthorViewModel : BaseViewModel
    {
        public string UserName { set; get; }

        public string NickName { set; get; }

        public int Fellow { set; get; }

        public int Fans { set; get; }

        public int Score { set; get; }

        public string Info { set; get; }

        public string UserInfoUrl { set; get; }

        public string UserWordUrl { set; get; }

        public string Source { set; get; }

        public string Description { set; get; }

        public string Skill { set; get; }

        public string Field { set; get; }

        public string Email { set; get; }

        public string Phone { set; get; }

        public string QQ { get; set; }

        public string WeiXin { get; set; }

        public EXArticleAuthorViewModel() { }

    }
}
