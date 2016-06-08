namespace ConsoleApplication4
{
    // Noktaların komşu bilgilerini tutmak içindir. 
    // Bir noktanın komşu noktasını tanımlarken
    // komşu nokta numarası, yön bilgisi ve 
    // komşu nokta ile aradaki mesafenin bilinmesi gereklidir.
    // Yön bilgisi "en kısa yol" probleminin çözümünde
    // özel olarak kullanılmadı ancak ağ bilgisinin
    // yönlü olarak tarif edilmesinde gerekli olabilir.
    // diğer problemlerde gerekebilir.

    public class Komsu
    {
        public enum KomsuYonu
        {
            Onceki,
            Sonraki,
        }

        public int NoktaId { get; set; }

        public KomsuYonu Yon { get; set; }

        public double Mesafe { get; set; }
    }
}
