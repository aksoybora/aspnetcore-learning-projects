// User: Uygulama kullanıcısı varlığı. Gönderiler ve yorumlarla ilişkilidir.
using System;

namespace BlogApp.Entity;

public class User
{
    public int UserId { get; set; } // Birincil anahtar
    public string? UserName { get; set; } // Görünen kullanıcı adı
    public string? Name { get; set; } // Ad Soyad
    public string? Email { get; set; } // E-posta adresi
    public string? Password { get; set; } // Parola (demo amaçlı düz metin)
    public string? Image { get; set; } // Profil görseli dosya adı
    public List<Post> Posts { get; set; } = new List<Post>(); // Kullanıcının yazdığı postlar
    public List<Comment> Comments { get; set; } = new List<Comment>(); // Kullanıcının yorumları
}
