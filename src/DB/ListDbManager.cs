using System;
using System.Collections.Generic;
using System.Linq;
using src.Models;

namespace src.DB
{
    public static class ListDbManager
    {
        #region Single List Methods
        public static AList OneList(int id)
        {
            using (var context = new ListDbContext())
            {
                var result = context.Lists.FirstOrDefault(l => l.ID == id);

                result.Items = context.ListItems.Where( li => li.AListID == id).ToList();

                return result;
            }
        }

        public static bool SaveList(AList save)
        {
            using (var context = new ListDbContext())
            {
                try
                {
                    save.PreSave();

                    if (save.ID == 0) { context.Lists.Add(save); }
                    else
                    {
                        context.Lists.Update(save);
                        ClearListItems(save.ID);
                    }

                    context.SaveChanges();
                    return true;
                }
                catch {}
            }
            return false;
        }

        public static bool RemoveList(int id)
        {
            return false;
        }

        public static bool ClearListItems(int id)
        {
            return false;
        }
        #endregion

        #region Multiple List Methods
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
        #endregion
    }
}