using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClosetItemApp.Api
{
    public class ClosetItemListDbContext : IdentityDbContext
    {
        public ClosetItemListDbContext(DbContextOptions<ClosetItemListDbContext> options) : base(options)
        {

        }

        public DbSet<ClosetItem> ClosetItems { get; set;  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClosetItem>().HasData(
                 new ClosetItem
                 {
                     Id = 1,
                     ShortName = "Silk pants",
                     ItemType = "Bottom",
                     Color = "Red"

                 },
                new ClosetItem
                {
                    Id = 2,
                    ShortName = "Jeans",
                    ItemType = "Bottom",
                    Color = "Blue"

                },
                new ClosetItem
                {
                    Id = 3,
                    ShortName = "Silk blouse",
                    ItemType = "Top",
                    Color = "Green"

                },
                new ClosetItem
                {
                    Id = 4,
                    ShortName = "Fur coat",
                    ItemType = "Outerwear",
                    Color = "Brown"

                },
                new ClosetItem
                {
                    Id = 5,
                    ShortName = "Leather jacket",
                    ItemType = "Outerwear",
                    Color = "Brown"

                },
                new ClosetItem
                {
                    Id = 6,
                    ShortName = "Pearl Necklace",
                    ItemType = "Accessory",
                    Color = "White"

                },
                new ClosetItem
                {
                    Id = 7,
                    ShortName = "Sweatshirt",
                    ItemType = "Top",
                    Color = "Pink"

                },
                new ClosetItem
                {
                    Id = 8,
                    ShortName = "Straightleg jeans",
                    ItemType = "Bottom",
                    Color = "Blue"

                },
                new ClosetItem
                {
                    Id = 9,
                    ShortName = "High Heels",
                    ItemType = "Footwear",
                    Color = "Black"

                },
                new ClosetItem
                {
                    Id = 10,
                    ShortName = "Polka-dot dress",
                    ItemType = "Dress",
                    Color = "Red/White"

                }

            );
        }
    }
}