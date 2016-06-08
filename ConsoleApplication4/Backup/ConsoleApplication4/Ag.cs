using System;
using System.Collections.Generic;



namespace ConsoleApplication4
{
    // Ağ yapısını temsil eder.
    // Ağ içinde nokta listesi içerir,
    // nokta ekleme, çıkarma, bağlantı ekleme, çıkarma gibi
    // metotları vardır.

    public class Ag
    {
        private Dictionary<int, Nokta> noktalar;

        public Ag()
        {
            noktalar = new Dictionary<int, Nokta>();
        }

        public IEnumerable<Nokta> NoktaListesi
        {
            get
            {
                return noktalar.Values;
            }
        }

        public int NoktaSayisi
        {
            get { return noktalar.Count; }
        }

        public Nokta NoktaAl(int NoktaId)
        {
            if (NoktaMi(NoktaId))
                return noktalar[NoktaId];

            return null;
        }

        public bool NoktaMi(int NoktaId)
        {
            return noktalar.ContainsKey(NoktaId);
        }

        public void NoktaEkle(int NoktaId)
        {
            if (NoktaMi(NoktaId)) return;

            noktalar.Add(NoktaId, new Nokta() { NoktaId = NoktaId, BirikimliMesafe = 0 });
        }

        public void NoktaCikar(int NoktaId)
        {
            // ağ üzerinde bir noktanın çıkarılması 
            // sadece ilgili noktanın nokta listesinden çıkarılması
            // ile olmaz, komşu bağlantılarının da güncellenmesi gereklidir.

            if (!NoktaMi(NoktaId)) return;

            var cikarilacakNokta = NoktaAl(NoktaId);

            var komsular = cikarilacakNokta.KomsuListesi;

            foreach (var kom in komsular)
            {
                var komNokta = noktalar[kom.NoktaId];

                komNokta.KomsuCikar(NoktaId);
            }

            noktalar.Remove(NoktaId);
        }

        public void BaglantiEkle(int baslangicNoktaId, int bitisNoktaId)
        {
            BaglantiEkle(baslangicNoktaId, bitisNoktaId, 0);
        }

        public void BaglantiEkle(int baslangicNoktaId, int bitisNoktaId, double mesafe)
        {
            // çevrimsel (cyclic ) bağlantıya izin verilmiyor

            if (baslangicNoktaId == bitisNoktaId)
                return;

            if (!(NoktaMi(baslangicNoktaId) && NoktaMi(bitisNoktaId)))
                return;

            var basNokta = noktalar[baslangicNoktaId];

            if (basNokta.KomsuMu(bitisNoktaId)) return;

            var bitNokta = noktalar[bitisNoktaId];

            basNokta.KomsuEkle(bitisNoktaId, Komsu.KomsuYonu.Sonraki, mesafe);

            bitNokta.KomsuEkle(baslangicNoktaId, Komsu.KomsuYonu.Onceki, mesafe);
        }

        

        public void BaglantiCikar(int baslangicNoktaId, int bitisNoktaId)
        {
            if (!(NoktaMi(baslangicNoktaId) && NoktaMi(bitisNoktaId)))
                return;

            var basNokta = noktalar[baslangicNoktaId];

            var bitNokta = noktalar[bitisNoktaId];

            basNokta.KomsuCikar(bitisNoktaId);

            bitNokta.KomsuCikar(baslangicNoktaId);
        }
    }
}
