using Microsoft.EntityFrameworkCore;


namespace ShoppingListTracker.Models
{
    public class ShopListContext : DbContext
    {
        public ShopListContext(DbContextOptions<ShopListContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationship between Item and Category
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)  // Each item belongs to a category
                .WithMany(c => c.Items)   // Each cat can have many items
                .HasForeignKey(i => i.CategoryId); // this is the foreign key in the items table
        }
    }
}
