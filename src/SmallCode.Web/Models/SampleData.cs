using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Web.Services;

namespace SmallCode.Web.Models
{
    public class SampleData
    {
        public async static Task InitDB(IServiceProvider service)
        {
            var db = service.GetService<SMContext>();
            var userService = service.GetService<IUserService>();

            if (db.Database != null && db.Database.EnsureCreated())
            {
                User user = new User
                {
                    UserName = "admin",
                    Password = "123456",
                    Email = "1941973283@qq.com",
                    Role = Role.管理员,
                };

                await userService.Save(user);
            }
        }
    }
}
