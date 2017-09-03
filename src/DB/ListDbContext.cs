using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
//using System.Reflection;
//using System.Linq.Expressions;
//using System.IO;
using src.Models;

namespace src.DB
{
    public class ListDbContext : DbContext
    {
        #region DbSets
        public DbSet<AList> Lists { get; set; }
        public DbSet<AList.AListItem> ListItems { get; set; }
        #endregion

        #region Properties
        public static DbContextOptions<ListDbContext> Options{ get; set; }
        #endregion

        #region Construct / Destruct
        public ListDbContext() : base(Options) { }

        public ListDbContext (DbContextOptions<ListDbContext> options) : base(options)
        {
            Options = options;
        }
        #endregion

        #region Static Methods
        public static void EnsureCreated(IServiceProvider serviceProvider)
        {
            var context = new ListDbContext(serviceProvider.GetRequiredService<DbContextOptions<ListDbContext>>());
            context.Database.EnsureCreated();
        }

        public static void DbInitialize(IServiceProvider serviceProvider)
        {
            using (var context = new ListDbContext(serviceProvider.GetRequiredService<DbContextOptions<ListDbContext>>()))
            {
                if (context.Lists.Any())
                {
                    return;   // DB already has something
                }

                // Always make sure something is in the database
                var oneList = new AList() { Title = "Sample List", LastUpdated = DateTime.Now };
                oneList.Items.Add(new AList.AListItem(){ Description="Learn DotNet Core", SortOrder=0 });
                oneList.Items.Add(new AList.AListItem(){ Description="Implement List Removal (Hint: ListDbManager.cs)", SortOrder=1 });
                oneList.Items.Add(new AList.AListItem(){ Description="Add Due Date to AListItem", SortOrder=2 });
                oneList.Items.Add(new AList.AListItem(){ Description="Add Sorting on Edit page", SortOrder=3 });
                oneList.Items.Add(new AList.AListItem(){ Description="Read the DeveloperGym blog", SortOrder=4 });
                context.Lists.Add(oneList);

                context.SaveChanges();
            }
        }
        #endregion
    }
}