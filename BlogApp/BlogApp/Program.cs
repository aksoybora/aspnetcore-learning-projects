// Program.cs: Uygulamanın başlangıç noktası. DI, EF Core, Authentication ve routing yapılandırmaları burada yapılır.
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// EF Core DbContext: Sqlite bağlantısı
builder.Services.AddDbContext<BlogContext>(options => { options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]); });

// Repository bağımlılıkları (Scoped ömür)
builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

// Cookie Authentication kurulumu (varsayılan şema)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/Users/Login";
});


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
// Kimlik doğrulama + yetkilendirme middleware
app.UseAuthentication();
app.UseAuthorization();

// Geliştirme/test için örnek verileri doldur
SeedData.FillTestDatas(app);

// localhost://posts/react-dersleri
// localhost://posts/php-dersleri
// localhost://posts/tag/web-programlama

app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new {controller = "Users", action = "Profile" }
);

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/details/{url}",
    defaults: new { controller = "Posts", action = "Details" }
);

app.MapControllerRoute(
    name: "posts_by_tag",
    pattern: "posts/tag/{tag}",
    defaults: new {controller = "Posts", action = "Index" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}"
);

app.Run();
