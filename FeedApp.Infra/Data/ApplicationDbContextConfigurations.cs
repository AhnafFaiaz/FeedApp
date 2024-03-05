using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FeedApp.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Infra.Data
{
    public class ApplicationDbContextConfigurations
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUser>().ToTable("Users");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            // Add any additional entity configurations here
            
        }

    }
}
