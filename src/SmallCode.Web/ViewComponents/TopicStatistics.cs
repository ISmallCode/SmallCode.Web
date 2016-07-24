using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SmallCode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.ViewComponents
{
    public class TopicStatistics : ViewComponent
    {
        private readonly SCContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "statistics";

        public TopicStatistics(SCContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var allstatistics = new Tuple<int, int, int>(0, 0, 0);
            if (!_memoryCache.TryGetValue(cachekey, out allstatistics))
            {
                var usercount = db.Users.Count();
                var topiccount = db.Topices.Count();
                var replycount = db.TopicReplies.Count();
                allstatistics = new Tuple<int, int, int>(usercount, topiccount, replycount);
                _memoryCache.Set(cachekey, allstatistics, TimeSpan.FromMinutes(1));
            }
            return View(allstatistics);
        }
    }
}
