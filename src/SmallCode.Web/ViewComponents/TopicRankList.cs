﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SmallCode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Web.ViewComponents
{
    public class TopicRankList : ViewComponent
    {
        private readonly SCContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "topicrank";

        public TopicRankList(SCContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<Topic>();
            if (!_memoryCache.TryGetValue(cachekey, out items))
            {
                items = GetRankTopics(10, 7);
                _memoryCache.Set(cachekey, items, TimeSpan.FromMinutes(10));
            }
            return View(items);
        }
        /// <summary>
        /// 获取主题排行
        /// </summary>
        /// <param name="top"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private List<Topic> GetRankTopics(int top, int days)
        {
            return db.Topices.Where(r => r.CreateDate > DateTime.Now.AddDays(-days))
                .OrderByDescending(r => r.Browses).Take(top).ToList();
        }
    }
}
