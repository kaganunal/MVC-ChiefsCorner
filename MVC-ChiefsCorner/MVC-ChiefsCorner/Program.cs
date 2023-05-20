//using ChiefsCorner.Management.Service.Configurations;
//using ChiefsCorner.Management.Service.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using MVC_ChiefsCorner.Context;
//using MVC_ChiefsCorner.Models;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<ChiefsCornerContext>((opt) =>
//{
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("CCContext"));
//});

//builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
//{

//}).AddEntityFrameworkStores<ChiefsCornerContext>().AddDefaultTokenProviders();

//builder.Services.Configure<IdentityOptions>(opt =>
//{
//    opt.ClaimsIdentity.RoleClaimType = "Admin";
//    opt.User.RequireUniqueEmail = true;
//    opt.SignIn.RequireConfirmedEmail = false;
//    opt.Password.RequireDigit = false;
//    opt.Password.RequireLowercase = false;
//    opt.Password.RequireUppercase = false;
//    opt.Password.RequireNonAlphanumeric = false;
//    opt.Password.RequiredLength = 3;

//    opt.ClaimsIdentity.RoleClaimType = "Customer";
//    opt.User.RequireUniqueEmail = true;
//    opt.SignIn.RequireConfirmedEmail = false;
//    opt.Password.RequireDigit = false;
//    opt.Password.RequireLowercase = false;
//    opt.Password.RequireUppercase = false;
//    opt.Password.RequireNonAlphanumeric = false;
//    opt.Password.RequiredLength = 3;
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = Configuration["JWT:ValidIssuer"],
//        ValidAudience = Configuration["JWT:ValidAudience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
//    };
//});
//builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfig").Get<EmailConfig>());

//builder.Services.AddScoped<IEmailService, EmailService>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();


using ChiefsCorner.Management.Service.Configurations;
using ChiefsCorner.Management.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC_ChiefsCorner.Context;
using MVC_ChiefsCorner.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ChiefsCornerContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CCContext"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{

}).AddEntityFrameworkStores<ChiefsCornerContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.ClaimsIdentity.RoleClaimType = "Admin";
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;

    opt.ClaimsIdentity.RoleClaimType = "Customer";
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfig").Get<EmailConfig>());
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


