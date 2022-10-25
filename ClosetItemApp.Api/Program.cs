using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });


            builder.Host.UseSerilog((ctx, lc) =>
                lc.WriteTo.Console()
                .ReadFrom.Configuration(ctx.Configuration));

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");

            

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
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);
                var tokenClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("email_confirmed", user.EmailConfirmed.ToString())
                }.Union(claims)
                .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var securityToken = new JwtSecurityToken(
                    issuer: builder.Configuration["JwtSettings:Issuer"],
                    audience: builder.Configuration["JwtSettings:Audience"],
                    claims: tokenClaims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(builder.Configuration["JwtSettings:DurationInMinutes"])),
                    signingCredentials: credentials
                );

                var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

                var response = new AuthResponseDto
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Token = accessToken
                };

                return Results.Ok(response);
            }).AllowAnonymous();


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