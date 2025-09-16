using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    public static class IdentitySeedData
    {
        // Uygulama ilk açıldığında test amaçlı bir yönetici (Admin) kullanıcısı oluşturmak için kullanılır.
        private const string adminUser = "Admin";
        private const string adminPassword = "Admin_123";

        // Uygulama başlangıcında Program.cs içinden çağrılır.
        // - Veritabanı için gerekli migration'lar uygulanır (Migrate)
        // - Admin kullanıcısı yoksa oluşturulur
        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            // Scoped servisleri çözümlemek için yeni bir scope oluşturup gerekli servisleri alıyoruz.
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

            // Eğer veritabanında uygulanmış migration varsa, veritabanını son sürüme taşır.
            // Not: Genellikle "Any()" kontrolü yerine doğrudan Migrate() çağrılır; burada koşullu kullanılmış.
            if(context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            // Identity kullanıcı işlemleri için UserManager servisini alıyoruz.
            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(adminUser);

            // Admin kullanıcısı yoksa yeni bir kullanıcı oluştur ve parola ata.
            if(user == null)
            {
                user = new IdentityUser {
                    UserName = adminUser,
                    Email = "admin@sadikturan.com",
                    PhoneNumber = "44444444"                    
                };

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    } 
}