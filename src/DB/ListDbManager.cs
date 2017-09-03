using System;
using System.Collections.Generic;
using System.Linq;
using src.Models;

namespace src.DB
{
    public static class ListDbManager
    {
        public static AList OneList(int id)
        {
            using (var context = new ListDbContext())
            {
                return context.Lists.FirstOrDefault(l => l.ID == id);
            }
        }

        public static List<AList> AllLists()
        {
            using (var context = new ListDbContext())
            {
                return context.Lists.ToList();
            }
        }

        public static List<AList> RecentLists()
        {
            using (var context = new ListDbContext())
            {
                return context.Lists.OrderByDescending(l => l.LastUpdated).Take(3).ToList();
            }
        }
    }
}