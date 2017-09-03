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
                var result = context.Lists.FirstOrDefault(l => l.ID == id); // Main list info
                result.Items = context.ListItems.Where( li => li.AListID == id).OrderBy(li => li.SortOrder).ToList(); // Sub items

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
                        ClearListItems(save.ID);
                        context.Lists.Update(save);
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

            // TODO: Implement this. Search online for EnitityFrameworkCore and delete object
            return false;
        }

        public static bool ClearListItems(int aListId)
        {
            using (var context = new ListDbContext())
            {
                try
                {
                    context.ListItems.RemoveRange(context.ListItems.Where(li => li.AListID == aListId));
                    context.SaveChanges();
                    return true;
                }
                catch{}
            }
            return false;
        }

        public static AList.AListItem OneListItem(int id)
        {
            using (var context = new ListDbContext())
            {
                var result = context.ListItems.FirstOrDefault(li => li.ID == id);

                return result;
            }
        }

        public static bool SaveListItem(AList.AListItem save)
        {
            using (var context = new ListDbContext())
            {
                try
                {
                    if (save.ID == 0) { context.ListItems.Add(save); }
                    else { context.ListItems.Update(save); }

                    context.SaveChanges();
                    return true;
                }
                catch {}
            }
            return false;
        }
        #endregion

        #region Multiple List Methods
        public static List<AList> AllLists()
        {
            using (var context = new ListDbContext())
            {
                return context.Lists.ToList(); // Header info only, for better performance
            }
        }

        public static List<AList> RecentLists()
        {
            using (var context = new ListDbContext())
            {
                return context.Lists.OrderByDescending(l => l.LastUpdated).Take(3).ToList(); // Header info only, for better performance
            }
        }
        #endregion
    }
}