using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DB
{
    public class ScraperDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ScraperDbContext(DbContextOptions<ScraperDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


        // NOTE: catageries MUST be added before items
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category).WithMany(cat => cat.Items);

            base.OnModelCreating(modelBuilder);
        }

        private void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            if (!e.FromQuery && e.Entry.State == EntityState.Added && e.Entry.Entity is Entity entity)
            {
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

        private void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Modified && e.Entry.Entity is Entity entity)
            {
                entity.ModifiedOn = DateTime.UtcNow;
            }
        }
    }
}

