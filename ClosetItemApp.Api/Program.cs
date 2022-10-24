using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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

            app.MapPut("/closetItems/{id}", async (int id, ClosetItem closetItem, ClosetItemListDbContext db) => {
                var record = await db.ClosetItems.FindAsync(id);
                if (record is null) return Results.NotFound();

                record.ShortName = closetItem.ShortName;
                record.ItemType = closetItem.ItemType;
                record.Color = closetItem.Color;

                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            app.MapDelete("/closetItems/{id}", async (int id, ClosetItemListDbContext db) => {
                var record = await db.ClosetItems.FindAsync(id);
                if (record is null) return Results.NotFound();
                db.Remove(record);
                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            app.MapPost("/closetItems", async (ClosetItem closetItem, ClosetItemListDbContext db) => {
                await db.AddAsync(closetItem);
                await db.SaveChangesAsync();

                return Results.Created($"/closetItems/{closetItem.Id}", closetItem);

            });


            app.Run();
        }
    }
}