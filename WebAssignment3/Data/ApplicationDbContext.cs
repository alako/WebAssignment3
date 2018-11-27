using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAssignment3.Models;

namespace WebAssignment3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ModelUser,ModelRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ComponentTypeCategory>()
                .HasKey(ctc => new { ctc.CategoryId, ctc.ComponentTypeId });

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(ctc => ctc.ComponentType)
                .WithMany(ct => ct.ComponentTypeCategories)
                .HasForeignKey(ctc => ctc.ComponentTypeId);

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(ctc => ctc.Category)
                .WithMany(c => c.ComponentTypeCategories)
                .HasForeignKey(ctc => ctc.CategoryId);

            //modelBuilder.Entity<User>()
            //    .HasIndex(user => new { user.Name }).IsUnique();
            
            //modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, Name = "Cat1" });
        }
        public DbSet<WebAssignment3.Models.ESImage> ESImage { get; set; }
        public DbSet<WebAssignment3.Models.Category> Category { get; set; }
        public DbSet<WebAssignment3.Models.Component> Component { get; set; }
        public DbSet<WebAssignment3.Models.ComponentType> ComponentType { get; set; }
        public DbSet<WebAssignment3.Models.ComponentTypeCategory> ComponentTypeCategory { get; set; }
    }
}
