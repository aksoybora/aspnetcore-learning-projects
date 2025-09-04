using System.ComponentModel.DataAnnotations; // Data validation attribute'ları için gerekli

namespace MeetingApp.Models
{
    // UserInfo sınıfı - kullanıcı bilgilerini temsil eden model
    // Bu sınıf hem veri saklama hem de form validasyonu için kullanılır
    public class UserInfo
    {
        // Kullanıcının benzersiz kimlik numarası
        // Auto-increment olarak Repository'de otomatik atanır
        public int Id { get; set; }

        // [Required] attribute'u - bu alanın zorunlu olduğunu belirtir
        // ErrorMessage: Validasyon hatası olduğunda gösterilecek mesaj
        // string? - nullable string, null değer alabilir
        [Required(ErrorMessage ="Ad alanı zorunlu")]
        public string?  Name { get; set; } // null

        // Telefon numarası - zorunlu alan
        [Required(ErrorMessage ="Telefon alanı zorunlu")]
        public string?  Phone { get; set; }
        
        // Email adresi - zorunlu alan
        // [EmailAddress] attribute'u - email formatını otomatik kontrol eder
        [Required(ErrorMessage ="Email alanı zorunlu")]
        [EmailAddress(ErrorMessage ="Hatalı email")]
        public string?  Email { get; set; }
        
        // Katılım durumu - zorunlu alan
        // bool? - nullable boolean, true/false/null değer alabilir
        [Required(ErrorMessage ="Katılım durumunuzu belirtiniz.")]
        public bool? WillAttend { get; set; } // true, false, null

        // YENİ: Favori durumu - kullanıcı favori olarak işaretlenebilir
        public bool IsFavorite { get; set; } = false;

        // YENİ: Son güncelleme tarihi - kullanıcı bilgilerinin ne zaman güncellendiği
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // YENİ: Kullanıcı notu - ek bilgiler için
        public string? Note { get; set; }
    }
}