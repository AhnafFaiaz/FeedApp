using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FeedApp.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal readonly object ProblemViewModel;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region DbSet Section
        public DbSet<Problems> Problems { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Reply> Reply { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ApplicationDbContextConfigurations.Configure(builder);
            

        }

    }
}
