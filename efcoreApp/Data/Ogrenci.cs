using System.ComponentModel.DataAnnotations;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciId { get; set; }
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }

        public string AdSoyad
        {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public ICollection<KursKayit> KursKayitları { get; set; } = new List<KursKayit>(); 
    }
}