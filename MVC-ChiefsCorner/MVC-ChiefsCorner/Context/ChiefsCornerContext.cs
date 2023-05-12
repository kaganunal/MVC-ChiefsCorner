using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_ChiefsCorner.Models;

namespace MVC_ChiefsCorner.Context
{
    public partial class ChiefsCornerContext : IdentityDbContext<IdentityUser>
    {
        public ChiefsCornerContext()
        {

        }

        public ChiefsCornerContext(DbContextOptions<ChiefsCornerContext> options)
            : base(options)
        {

        }

        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<MenuExtra> MenuExtras { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenu> OrderMenus { get; set; }
        public DbSet<OrderExtra> OrderExtras { get; set; }
        public Size Size { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuCategory>()
            .HasMany(mc => mc.Menus)
            .WithOne(m => m.MenuCategory)
            .HasForeignKey(m => m.MenuCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Menu>()
                .HasMany(m => m.MenuExtras)
                .WithOne(me => me.Menu)
                .HasForeignKey(me => me.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Extra>()
                .HasMany(e => e.MenuExtras)
                .WithOne(me => me.Extra)
                .HasForeignKey(me => me.ExtraId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuExtra>()
                .HasKey(me => new { me.MenuId, me.ExtraId });

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderMenus)
                .WithOne(om => om.Order)
                .HasForeignKey(om => om.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderExtras)
                .WithOne(oe => oe.Order)
                .HasForeignKey(oe => oe.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderMenu>()
                .HasMany(om => om.OrderExtras)
                .WithOne(oe => oe.OrderMenu)
                .HasForeignKey(oe => new { oe.OrderId, oe.OrderMenuId })
                .OnDelete(DeleteBehavior.Cascade);

            OnModelCreatingPartial(modelBuilder);
            AddRoles(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private static void AddRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole()
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                }
                );

        }
    }
}
