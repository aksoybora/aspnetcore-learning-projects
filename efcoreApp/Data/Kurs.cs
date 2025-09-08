using efcoreApp.Data;
namespace efcoreApp.Data
{
    public class Kurs
    {
        public int KursId { get; set; }
        public string? Baslik { get; set; }
        public int? OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!;
        public ICollection<KursKayit> KursKayitları { get; set; } = new List<KursKayit>();
    }
}