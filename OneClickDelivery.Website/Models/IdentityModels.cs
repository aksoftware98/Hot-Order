using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OneClickDelivery.Models;

namespace OneClickDelivery.Website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<AppType> AppTypes { get; set; }
        public DbSet<AppVersion> AppVersions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuSection> MenuSections { get; set; }
        public DbSet<MenuType> MenuTypes { get; set; }
        public DbSet<Resturant> Resturants { get; set; }
        public DbSet<Faq> FAQs { get; set; }
        public DbSet<Statstic> Statstics { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<BlockedCountry> BlockedCountries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Set one to one relationship between resturant and menu 
            modelBuilder.Entity<Resturant>().HasOptional(p => p.Menu).WithRequired(p => p.Resturant).WillCascadeOnDelete();

            // Set one to may relationship between menu and menu sections 
            modelBuilder.Entity<MenuSection>().HasRequired(p => p.Menu).WithMany(m => m.MenuSections).WillCascadeOnDelete(); 

            // Set one to one relationship between shopping cart and order
            modelBuilder.Entity<ShoppingCart>().HasOptional(p => p.Order).WithRequired(p => p.ShoppingCart).WillCascadeOnDelete();

            // Set one to many relationshiop between item and cart items 
            modelBuilder.Entity<CartItem>().HasRequired(p => p.Item).WithMany(p => p.CartItems).WillCascadeOnDelete(); 

            // Set one to one relationship between shopping cart and order 
            //modelBuilder.Entity<Order>().HasRequired(p => p.ShoppingCart).

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}