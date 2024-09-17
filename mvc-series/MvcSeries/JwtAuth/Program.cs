using JwtAuth.Data;
using JwtAuth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//string connectionString = Environment.GetEnvironmentVariable("BookStoreConnectionString") ?? throw new InvalidOperationException("Connection string 'BookStoreConnectionString' not found.");
string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"] ?? throw new ArgumentNullException(nameof(args));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["https://localhost:7188/"],
            ValidAudience = builder.Configuration["https://localhost:7188/"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""))
        };
    });

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(60); // Set session timeout
//});

//// JWT Authentication Configuration
//var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? throw new InvalidDataException());

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//        ValidAudience = builder.Configuration["JwtSettings:Audience"],
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero
//    };

//    // This handles redirecting to login page when unauthorized
//    x.Events = new JwtBearerEvents
//    {
//        OnChallenge = context =>
//        {
//            context.HandleResponse();               // Skip the default response
//            context.Response.Redirect("/Login");    // Redirect to login page
//            return Task.CompletedTask;
//        }
//    };
//});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseSession();           // Enable session
app.UseAuthentication();    // Enable JWT Authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
