using System;
using System.Collections.Generic;



namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Başladı");

            // Ağ yapısında 1->2, 2->3 ve 1->3 bağlantıları olacak.
            // 1->2 mesafesi 5 birim, 2->3 mesafesi 3 birim, 1->3 mesafesi 10 birimdir.
            // Çözüm yolu bu değerler ile 1->2->3 şeklindedir.
            // 2->3 mesafesini 6 veya daha büyük değer yaparsanız çözüm yolu
            // 1->3 olarak değişir

            var ag = new Ag();

            ag.NoktaEkle(1);
            
            ag.NoktaEkle(2);
            
            ag.NoktaEkle(3);

            ag.BaglantiEkle(1, 2, 5);

            ag.BaglantiEkle(2, 3, 3);

            ag.BaglantiEkle(1, 3, 10);

            var alg = new AgAlgoritmalari();

            List<Nokta> cozum = new List<Nokta>();

            alg.EnKisaYolHesapla(ag, 1, 3, cozum);

            Console.WriteLine("İzlenen yol");

            foreach (var nokta in cozum)
            {
                Console.WriteLine("Nokta no: {0}", nokta.NoktaId);
            }

            Console.WriteLine("Bitti");

            Console.WriteLine("Press any key to exit, do not search for the \"ANY\" key on keyboard :)");

            Console.ReadKey();            
            
        }        
    }
}
