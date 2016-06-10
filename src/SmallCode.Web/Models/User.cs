using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.Models
{
    public class User
    {
        public Guid Id { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }

        public DateTime CreatedDate { set; get; }

        public string Email { set; get; }

        public Role Role { set; get; }
    }
}
