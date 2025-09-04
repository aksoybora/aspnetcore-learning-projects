namespace MeetingApp.Models
{
    // MeetingInfo sınıfı - toplantı bilgilerini temsil eden model
    // Bu sınıf toplantı detaylarını saklamak için kullanılır
    public class MeetingInfo
    {
        // Toplantının benzersiz kimlik numarası
        public int Id { get; set; }
        
        // Toplantının yapılacağı yer
        // string? - nullable string, null değer alabilir
        public string? Location { get; set; }
        
        // Toplantının tarihi ve saati
        // DateTime: Tarih ve saat bilgisini birlikte saklar
        public DateTime Date { get; set; }
        
        // Toplantıya katılacak kişi sayısı
        // Repository'den hesaplanan dinamik değer
        public int NumberOfPeople { get; set; }
    }
}