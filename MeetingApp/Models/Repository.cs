namespace MeetingApp.Models
{
    // Repository sınıfı - veri erişim katmanı olarak kullanılır
    // Static olarak tanımlanmış çünkü uygulama boyunca tek bir instance yeterli
    public static class Repository
    {
        // Kullanıcı bilgilerini saklayan private liste
        // Private: Sadece bu sınıf içinden erişilebilir
        // Static: Sınıf seviyesinde tanımlanmış, her instance'da ayrı değil
        private static List<UserInfo> _users = new();

        // Static constructor - sınıf ilk kez kullanıldığında çalışır
        // Sadece bir kez çalışır ve örnek veriler ekler
        static Repository()
        {
            // Örnek kullanıcı verileri ekleniyor
            // Her kullanıcı için Id, Name, Email, Phone ve WillAttend bilgileri
            _users.Add(new UserInfo() { 
                Id=1, 
                Name = "Ali", 
                Email = "ali@gmail.com", 
                Phone = "11111", 
                WillAttend=true,
                IsFavorite = true,
                Note = "VIP katılımcı"
            });
            _users.Add(new UserInfo() { 
                Id=2, 
                Name = "Ahmet", 
                Email = "ahmet@gmail.com", 
                Phone = "22222", 
                WillAttend=false,
                IsFavorite = false,
                Note = "Geçen yıl katılmıştı"
            });
            _users.Add(new UserInfo() { 
                Id=3, 
                Name = "Canan", 
                Email = "canan@gmail.com", 
                Phone = "33333", 
                WillAttend=true,
                IsFavorite = true,
                Note = "İlk kez katılıyor"
            });
        }

        // Users property'si - dışarıdan kullanıcı listesine erişim sağlar
        // Get-only property: Sadece okuma yapılabilir, yazma yapılamaz
        public static List<UserInfo> Users {
            get {
                return _users; // Private _users listesini döndürür
            }
        }

        // Yeni kullanıcı ekleme metodu
        // Parametre olarak UserInfo tipinde bir kullanıcı alır
        public static void CreateUser(UserInfo user)
        {
            // Yeni kullanıcıya otomatik ID atama
            // Mevcut kullanıcı sayısı + 1 olarak ID verilir
            user.Id = Users.Count + 1;
            // Kullanıcıyı listeye ekleme
            _users.Add(user);
        }        

        // ID'ye göre kullanıcı bulma metodu
        // Parametre olarak int tipinde ID alır
        // Nullable UserInfo döndürür (kullanıcı bulunamazsa null)
        public static UserInfo? GetById(int id)
        {
            // LINQ kullanarak ID'ye göre kullanıcı arama
            // FirstOrDefault: İlk eşleşen kullanıcıyı bulur, bulamazsa null döner
            return _users.FirstOrDefault(user => user.Id == id);
        }

        // YENİ: Favori durumunu değiştirme metodu
        public static void ToggleFavorite(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                user.IsFavorite = !user.IsFavorite;
                user.LastUpdated = DateTime.Now;
            }
        }

        // YENİ: İsme göre kullanıcı arama metodu
        public static List<UserInfo> SearchByName(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _users;
            
            return _users.Where(user => 
                user.Name?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true
            ).ToList();
        }

        // YENİ: Favori kullanıcıları getirme metodu
        public static List<UserInfo> GetFavorites()
        {
            return _users.Where(user => user.IsFavorite).ToList();
        }

        // YENİ: İstatistik bilgilerini getirme metodu
        public static object GetStatistics()
        {
            return new
            {
                TotalUsers = _users.Count,
                Attending = _users.Count(u => u.WillAttend == true),
                NotAttending = _users.Count(u => u.WillAttend == false),
                Uncertain = _users.Count(u => u.WillAttend == null),
                Favorites = _users.Count(u => u.IsFavorite),
                RecentUpdates = _users.Count(u => u.LastUpdated > DateTime.Now.AddDays(-7))
            };
        }

        // YENİ: Kullanıcı notu ekleme/güncelleme metodu
        public static void UpdateNote(int id, string note)
        {
            var user = GetById(id);
            if (user != null)
            {
                user.Note = note;
                user.LastUpdated = DateTime.Now;
            }
        }
    }
}