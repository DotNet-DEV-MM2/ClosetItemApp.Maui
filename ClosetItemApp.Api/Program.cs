using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ClosetItemApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            var conn = new SqliteConnection($"Data Source=C:\\closetitemlistdb\\closetitemlist.db");
            builder.Services.AddDbContext<ClosetItemListDbContext>(o => o.UseSqlite(conn));

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ClosetItemListDbContext>();

            builder.Host.UseSerilog((ctx, lc) =>
                lc.WriteTo.Console()
                .ReadFrom.Configuration(ctx.Configuration));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapGet("/closetItems", async (ClosetItemListDbContext db) => await db.ClosetItems.ToListAsync());

            app.MapGet("/closetItems/{id}", async (int id, ClosetItemListDbContext db) =>
                await db.ClosetItems.FindAsync(id) is ClosetItem closetItem ? Results.Ok(closetItem) : Results.NotFound()
            );

            app.MapPut("/closetItems/{id}", async (int id, ClosetItem closetItem, ClosetItemListDbContext db) =>
            {
                var record = await db.ClosetItems.FindAsync(id);
                if (record is null) return Results.NotFound();

                record.ShortName = closetItem.ShortName;
                record.ItemType = closetItem.ItemType;
                record.Color = closetItem.Color;

                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            app.MapDelete("/closetItems/{id}", async (int id, ClosetItemListDbContext db) =>
            {
                var record = await db.ClosetItems.FindAsync(id);
                if (record is null) return Results.NotFound();
                db.Remove(record);
                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            app.MapPost("/closetItems", async (ClosetItem closetItem, ClosetItemListDbContext db) =>
            {
                await db.AddAsync(closetItem);
                await db.SaveChangesAsync();

                return Results.Created($"/closetItems/{closetItem.Id}", closetItem);

            });

            app.MapPost("/login", async (LoginDto loginDto, UserManager<IdentityUser> _userManager) =>
            {
                var user = await _userManager.FindByNameAsync(loginDto.Username);

                if (user is null)
                {
                    return Results.Unauthorized();
                }

                var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (!isValidPassword)
                {
                    return Results.Unauthorized();
                }

                // Generate an access token

                var response = new AuthResponseDto
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Token = "AccessTokenHere"

                };

                return Results.Ok(response);
            });


            app.Run();
        }

        internal class LoginDto
        {
            public string Username { get; set; } 
            public string Password { get; set; }
        }

        internal class AuthResponseDto

        {
            public string UserId { get; set; }
            public string Username { get; set; }
            public string Token { get; set; }

        }
    }
}