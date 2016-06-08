using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApplication4
{
    public class AgAlgoritmalari
    {
        private class MesafeSirala: IComparer<Nokta> {

            public int Compare(Nokta x, Nokta y)
            {
                return x.BirikimliMesafe.CompareTo(y.BirikimliMesafe);
            }
        }

        public void EnKisaYolHesapla(Ag ag, int baslangicNoktaId, int bitisNoktaId, List<Nokta> yol) 
        {
            yol.Clear();

            if (!(ag.NoktaMi(baslangicNoktaId) && ag.NoktaMi(bitisNoktaId)))
                return;

            var noktalar = ag.NoktaListesi;

            // tüm birikimli mesafelere sonsuz büyüklükte değer atanıyor.
            // her adımda bunlar azaltılacak.            
            // her noktanın önceki nokta değeri temizleniyor

            foreach (var nokta in noktalar)
            {
                nokta.BirikimliMesafe = Double.MaxValue;

                nokta.OncekiNoktaId = 0;
            }

            // başlangıç noktasının birikimli mesafesi sıfır yapılıyor.

            var baslangicNokta = ag.NoktaAl(baslangicNoktaId);

            baslangicNokta.BirikimliMesafe = 0;

            var islenecekler = new List<Nokta>();

            var islenenler = new HashSet<int>();

            islenecekler.Add(baslangicNokta);

            var siralayici = new MesafeSirala();

            while (islenecekler.Count > 0)
            {
                var nokta = islenecekler[0];

                islenecekler.RemoveAt(0);

                if (islenenler.Contains(nokta.NoktaId))
                    continue;

                islenenler.Add(nokta.NoktaId);

                // varış noktasına ulaşıldı ise işlemi sonlandır,
                // çözüme ulaşılmıştır.

                if (nokta.NoktaId == bitisNoktaId)
                    break;

                var komsular = nokta.KomsuListesi;

                foreach (var komsu in komsular)
                {
                    // komşu nokta daha önce işlendi ise tekrar kontrol etme.
                    // ağ üzerinde geri gitmeyi engellemek içindir.
                    // normalde ağdaki bağlantılar yönlü olsa da çevrimsel (cyclic)
                    // ağ durumunda tekrar başlangıç noktasına ya da önceden 
                    // işlenmiş noktalara geri dönmemek için eklendi.

                    if (islenenler.Contains(komsu.NoktaId))
                        continue;

                    var komsuNokta = ag.NoktaAl(komsu.NoktaId);

                    // komşu noktaya bu noktadan ulaşmak daha uygun ise
                    // komşu noktanın birikimli mesafesini ve ona ulaşılan 
                    // önceki noktayı güncele.

                    double yenimesafe = nokta.BirikimliMesafe + komsu.Mesafe;

                    if (yenimesafe < komsuNokta.BirikimliMesafe)
                    {
                        komsuNokta.BirikimliMesafe = yenimesafe;

                        komsuNokta.OncekiNoktaId = nokta.NoktaId;

                        islenecekler.Add(komsuNokta);
                    }                    
                }

                // bir sonraki adıma geçmeden önce işlenecek noktaları 
                // birikimli mesafelerine göre sırala, en küçük
                // birikimli mesafeli noktadan devam edilsin.

                islenecekler.Sort(siralayici);
            }

            // işlenen nokta sayısı, ağdaki nokta sayısından az ise
            // başlangıç ve bitiş noktaları ağda bağlantılı değildir

            if (islenenler.Count < ag.NoktaSayisi)
                return;

            // son noktadan geri giderek çözüm yolunu üret.

            var bitisNokta = ag.NoktaAl(bitisNoktaId);

            yol.Insert(0, bitisNokta);

            var noktaId = bitisNokta.OncekiNoktaId;

            while (true)
            {
                var oncekiNokta = ag.NoktaAl(noktaId);

                if (oncekiNokta == null)
                    break;

                yol.Insert(0, oncekiNokta);

                noktaId = oncekiNokta.OncekiNoktaId;
            }
        }
    }
}
