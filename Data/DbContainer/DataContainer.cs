using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DbContainer
{
    public class DataContainer : DbContext
    {
        public DataContainer(DbContextOptions<DataContainer> options ):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(e => e.Id).HasDefaultValueSql("NewID()");
            modelBuilder.Entity<ProfileItem>().Property(e => e.Id).HasDefaultValueSql("NewID()");
            modelBuilder.Entity<Owner>().HasData(new Owner
            {   
                Id= Guid.NewGuid(), 
                Avatar = "Avatar.jpeg",
                FullName = "Mahmoud Youssef",
                Title = "Software Engineer",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }); 
        }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<ProfileItem> profileItems { get; set; }

    }
}
