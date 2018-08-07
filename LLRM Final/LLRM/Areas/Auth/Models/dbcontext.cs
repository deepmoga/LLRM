using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Admin2.Models
{
    public class dbcontext:DbContext
    {
        public dbcontext() : base("dbcontext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<dbcontext, LLRM.Migrations.Configuration>("dbcontext"));
        }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.slider> sliders { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.News> News { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Campuslife> Campuslives { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Tour> Tours { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Pages> Pages { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.department> departments { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.infrastructure> infrastructures { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Album> Albums { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Gallery> Galleries { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Video> Videos { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Announcement> Announcements { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.infrastructureData> infrastructureDatas { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.InfraGallery> InfraGalleries { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.CourseGallery> CourseGalleries { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.departmentdata> departmentdatas { get; set; }

        public System.Data.Entity.DbSet<LLRM.Areas.Auth.Models.DepGallery> DepGalleries { get; set; }
    }
}