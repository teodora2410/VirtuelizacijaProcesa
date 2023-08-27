using Common;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace VirtuelizacijaProcesa
{
    public class SlanjePaketa
    {
        private readonly ConcurrentBag<string> importedFiles = new ConcurrentBag<string>();

        private readonly TipBaze tipBaze;
        private readonly string putanja;
        private readonly ILoad proxy;

        public SlanjePaketa(TipBaze tipBaze, string putanja, ILoad proxy)
        {
            this.tipBaze = tipBaze;
            this.putanja = putanja;
            this.proxy = proxy;
        }


        public void PosaljiPakete()
        {
            string[] fajlovi = DobaviSveCsv(putanja);
            foreach (string fajl in fajlovi)
            {
                PosaljiPaket(fajl);
            }
        }

        public static string[] DobaviSveCsv(string putanja)
        {
            return Directory.GetFiles(putanja, "*.csv", SearchOption.TopDirectoryOnly);
        }


        private MemoryStream NapraviMemoryStream(string putanja)
        {
            // IMPLEMENTIRATI
            MemoryStream ms = new MemoryStream();
            using (FileStream fileStream = new FileStream(putanja, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(ms);
                fileStream.Close();
            }
            return ms;
        }

        public void PosaljiPaket(string fajl)
        {
            if (importedFiles.Contains(fajl))
            {
                Console.WriteLine($"Fajl {Path.GetFileName(fajl)} je vec poslat.");
                return;
            }
            var imeFajla = Path.GetFileName(fajl);
            PaketiZaSlanje paket = new PaketiZaSlanje(NapraviMemoryStream(fajl), imeFajla, tipBaze);

            try
            {
                var rezultat = proxy.UcitajLoad(paket);

                paket.Dispose();

                if (rezultat.TipRezultata == TipRezultata.Neuspesno)
                {
                    Console.WriteLine($"Fajl {imeFajla} nije uspesno prosledjen. Error: {rezultat.Poruka}");
                }
                else
                {
                    Console.WriteLine($"Fajl {imeFajla} uspesno prosledjen.");
                    importedFiles.Add(putanja);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
