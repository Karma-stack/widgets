﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using widgets.Data.Entities;

namespace widgets.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable(nameof(Users));
            modelBuilder.Entity<Role>().ToTable(nameof(Roles));

            modelBuilder.Ignore(typeof(IdentityUserClaim<>));
            modelBuilder.Ignore(typeof(IdentityRoleClaim<>));
            modelBuilder.Ignore(typeof(IdentityUserLogin<>));
            modelBuilder.Ignore(typeof(IdentityUserToken<>));
        }

    }
}
