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
        public Size Size { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderMenu>()
                .HasOne(om => om.Order)
                .WithMany(o => o.OrderMenus)
                .HasForeignKey(om => om.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderMenu>()
                .HasOne(om => om.Menu)
                .WithMany(m => m.OrderMenus)
                .HasForeignKey(om => om.MenuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuExtra>()
                .HasKey(me => new { me.MenuId, me.ExtraId });

            modelBuilder.Entity<MenuExtra>()
                .HasOne(me => me.Menu)
                .WithMany(m => m.MenuExtras)
                .HasForeignKey(me => me.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuExtra>()
                .HasOne(me => me.Extra)
                .WithMany(e => e.MenuExtras)
                .HasForeignKey(me => me.ExtraId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.MenuCategory)
                .WithMany(c => c.Menus)
                .HasForeignKey(m => m.MenuCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

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
