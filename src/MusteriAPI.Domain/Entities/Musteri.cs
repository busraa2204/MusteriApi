namespace MusteriAPI.Domain.Entities
{
    public class Musteri
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string TelefonNumarasi { get; set; }
        public string Adres { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
} 