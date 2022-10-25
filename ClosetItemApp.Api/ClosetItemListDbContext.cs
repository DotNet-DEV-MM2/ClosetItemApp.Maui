using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClosetItemApp.Api
{
    public class ClosetItemListDbContext : IdentityDbContext
    {
        public ClosetItemListDbContext(DbContextOptions<ClosetItemListDbContext> options) : base(options)
        {

        }

        public DbSet<ClosetItem> ClosetItems { get; set; }

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

            modelBuilder.Entity<IdentityRole>().HasData(
                           new IdentityRole
                           {
                               Id = "d1b5952a-2162-46c7-b29e-1a2a68922c14",
                               Name = "Administrator",
                               NormalizedName = "ADMINISTRATOR"
                           },
                           new IdentityRole
                           {
                               Id = "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                               Name = "User",
                               NormalizedName = "USER"
                           }
                       );

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                    new IdentityUser
                    {
                        Id = "408aa945-3d84-4421-8342-7269ec64d949",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        UserName = "admin@localhost.com",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                        EmailConfirmed = true
                    },
                    new IdentityUser
                    {
                        Id = "3f4631bd-f907-4409-b416-ba356312e659",
                        Email = "user@localhost.com",
                        NormalizedEmail = "USER@LOCALHOST.COM",
                        NormalizedUserName = "USER@LOCALHOST.COM",
                        UserName = "user@localhost.com",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                        EmailConfirmed = true
                    }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                     new IdentityUserRole<string>
                     {
                         RoleId = "d1b5952a-2162-46c7-b29e-1a2a68922c14",
                         UserId = "408aa945-3d84-4421-8342-7269ec64d949",
                     },
                    new IdentityUserRole<string>
                    {
                        RoleId = "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                        UserId = "3f4631bd-f907-4409-b416-ba356312e659",
                    }
                );
        }
    }
}