using System.Collections.Generic;


namespace ConsoleApplication4
{
    // Ağ üzerindeki bir noktayı temsil eder.
    // Her noktanın bir tekil kimlik numarası olmalıdır,
    // bu numara ağ üzerinde her nokta için farklıdır.
    // Nokta üzerinde komşu listesi tutulur.
    // Komşu listesi performans sebebiyle Dictionary olarak 
    // tanımlanmıştır, anahtar değeri nokta numarasıdır.

    public class Nokta
    {
        public int NoktaId { get; set; }

        private Dictionary<int, Komsu> komsular;

        public Nokta()
        {
            komsular = new Dictionary<int, Komsu>();
        }

        // En kısa yol algoritmasında bu noktaya ilk noktadan 
        // ulaşmanın toplam maliyetini tutar.
        public double BirikimliMesafe { get; set; }

        // En kısa yol algoritmasında bu noktaya hangi noktadan
        // gelindiğini tutar.
        public int OncekiNoktaId { get; set; }

        // Noktanın komşu noktalarını tutar.
        public IEnumerable<Komsu> KomsuListesi
        {
            get
            {
                return komsular.Values;
            }
        }

        public void KomsuListesiTemizle()
        {
            komsular.Clear();
        }

        public Komsu KomsuAl(int NoktaId)
        {
            if (KomsuMu(NoktaId))
                return komsular[NoktaId];

            return null;
        }

        public bool KomsuMu(int NoktaId)
        {
            return komsular.ContainsKey(NoktaId);
        }

        public void KomsuEkle(int NoktaId, Komsu.KomsuYonu Yon)
        {
            KomsuEkle(NoktaId, Yon, 0);
        }

        public void KomsuEkle(int NoktaId, Komsu.KomsuYonu Yon, double mesafe)
        {
            if (KomsuMu(NoktaId)) return;

            komsular.Add(NoktaId, new Komsu() { NoktaId = NoktaId, Yon = Yon, Mesafe = mesafe });
        }

        public void KomsuCikar(int NoktaId)
        {
            if (!KomsuMu(NoktaId)) return;

            komsular.Remove(NoktaId);
        }

    }
}
