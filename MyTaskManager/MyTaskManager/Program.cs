using Microsoft.EntityFrameworkCore;
using MyTaskManager.Data;
using MyTaskManager.Middleware;
using MyTaskManager.Models;
using MyTaskManager.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.Cookie.Name = "jwt";
        options.Events.OnValidatePrincipal = async context =>
        {
            var token = context.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                context.RejectPrincipal();
            }

            var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
            var user = tokenService.ValidateToken(token);

            if (user == null)
            {
                context.RejectPrincipal();
            }
            else
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(claimsIdentity);
                context.ReplacePrincipal(principal);
                context.ShouldRenew = true;
            }
        };
    });

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtMiddleware>();

app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
