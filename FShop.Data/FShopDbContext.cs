using FShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data
{
    public class FShopDbContext : DbContext
    {
        //step 1
        //khai bao cac bang entity ben trong
        public FShopDbContext():base("FShop")
        {
            //ko load bang con
            this.Configuration.LazyLoadingEnabled = false;
        }
        /*===Footer===*/
        public DbSet<Footer> Footers { set; get; }

        /*===Menu===*/
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }

        /*===Order===*/
        //public DbSet<Order> Orders { set; get; }
        //public DbSet<OrderDetail> OrderDetails { set; get; }

        /*===Page===*/
        public DbSet<Page> Pages { set; get; }

        /*===Post===*/
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }

        /*===Product===*/
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }

        /*===Slide===*/
        public DbSet<Slide> Slides { set; get; }

        /*===SupportOnline===*/
        public DbSet<SupportOnline> SupportOnlines { set; get; }

        /*===SystemConfig===*/
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        /*===Tag===*/
        public DbSet<Tag> Tags { set; get; }

        /*===VisitorStatistic===*/
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }

        /*===Error===*/
        public DbSet<Error> Errors { set; get; }

        /*===ContactDetail===*/
        public DbSet<ContactDetail> ContactDetails { set; get; }

        /*===Feedback===*/
        public DbSet<Feedback> Feedbacks { set; get; }

        /*===ApplicationGroup===*/
        //public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        //public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        //public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        //public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        //ghi de db context
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
