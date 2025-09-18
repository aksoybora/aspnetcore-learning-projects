using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    // Identity tabloları (Users, Roles, Claims vb.) için EF Core DbContext sınıfı.
    // IdentityDbContext<AppUser> kalıtımı ile ASP.NET Core Identity'nin hazır şemasını kullanır.
    public class IdentityContext: IdentityDbContext<AppUser, AppRole, string>
    {
        // DI (Dependency Injection) ile dışarıdan verilen DbContextOptions parametresi
        // üzerinden sağlayıcı (Sqlite) ve bağlantı dizesi gibi ayarlar gelir.
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {
            
        }
    }
}