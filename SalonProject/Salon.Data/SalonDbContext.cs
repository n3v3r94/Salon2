using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Salon.Data.Models;




namespace Salon.Data
{
    public class SalonDbContext : IdentityDbContext<User>
    {
        public SalonDbContext(DbContextOptions<SalonDbContext> options)
            : base(options)
        {

         }

        public DbSet<Salons> Salons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> DbUsers { get; set; }
        public DbSet<WorkerProduct> WorkerProduct { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }

        

        protected override void OnModelCreating(ModelBuilder builder)
         {

            builder.Entity<Salons>().HasMany(p => p.Products).WithOne(s => s.Salon).HasForeignKey(fk => fk.SalonId);
            builder.Entity<Salons>().HasOne(u => u.User).WithMany(s => s.Salon).HasForeignKey(u => u.UserId);
            builder.Entity<Salons>().Property(s => s.RowVersion).IsConcurrencyToken();

            builder.Entity<Product>().Property(s => s.RowVersion).IsConcurrencyToken();

            builder.Entity<WorkerProduct>().HasKey(pw => new { pw.ProductId, pw.WorkerId });
            builder.Entity<WorkerProduct>().HasOne(w => w.Worker).WithMany(p => p.Products).HasForeignKey(f => f.WorkerId);
            builder.Entity<WorkerProduct>().HasOne(p => p.Product).WithMany(w => w.Workers).HasForeignKey(f => f.ProductId);

            builder.Entity<Worker>().HasIndex(u => u.userId).IsUnique();
            builder.Entity<Worker>().HasMany(e => e.Events).WithOne(w => w.Worker).HasForeignKey(fw => fw.WorkerId);
            builder.Entity<Worker>().HasMany(w => w.WorkTimes).WithOne(w => w.Worker).HasForeignKey(w => w.WorkerId);
           
            base.OnModelCreating(builder);
           
        }
    }
}
