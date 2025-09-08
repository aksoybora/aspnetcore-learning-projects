using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;


// DataContext (Veritabanı Bağlamı)
// DataContext.cs dosyası Entity Framework Core'un kalbi gibidir. Bu sınıf:
// Veritabanı bağlantısını yönetir - SQLite veritabanına nasıl bağlanacağını bilir
// Model sınıflarını veritabanı tablolarıyla eşler - Ogrenci sınıfı ↔ Ogrenciler tablosu
// CRUD işlemlerini sağlar - Create, Read, Update, Delete


namespace efcoreApp.Data
{
    public class DataContext: DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>(); // Bu satır Ogrenci sınıfını Ogrenciler adında bir veritabanı tablosuyla eşler.
        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();
    }
}