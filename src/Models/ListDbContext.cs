using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
//using System.Reflection;
//using System.Linq.Expressions;
//using System.IO;

namespace src.Models
{
    public class ListDbContext : DbContext
    {
        #region DbSets
        public DbSet<AList> Lists { get; set; }
        #endregion

        #region Properties
        public static DbContextOptions<ListDbContext> Options{ get; set; }
        #endregion

        public ListDbContext (DbContextOptions<ListDbContext> options) : base(options)
        {
            Options = options;
        }

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
                var oneList = new AList() { Title = "Sample List" };
                oneList.Items.Add(new AList.AListItem(){ Description="Learn DotNet Core" });
                oneList.Items.Add(new AList.AListItem(){ Description="Add DateTime to AListItem" });
                oneList.Items.Add(new AList.AListItem(){ Description="Read the blog" });
                context.Lists.Add(oneList);

                context.SaveChanges();
            }
        }
    }
}