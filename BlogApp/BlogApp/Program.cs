using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:sql_connection"]);
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();

var app = builder.Build();

app.UseStaticFiles();

SeedData.FillTestDatas(app);

app.MapDefaultControllerRoute();

app.Run();
