using BazaPodataka;
using Common;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Server
{
    public enum TipLoada
    {
        Forecast,
        Measured
    }

    public class Servis : ILoad
    {
        private IBaza baza;
        private int fileId = -1;
        private int redUCsv = 1;
        private int ukupanBrojRedova = 0;
        private bool bazaPodataka = false;
        public delegate void AzurirajBazuPodataka(Load load);
        public event AzurirajBazuPodataka AzurirajBazu;

        public RezultatSlanja UcitajLoad(PaketiZaSlanje paket)
        {
            RezultatSlanja rezultat;
            if (!bazaPodataka)
            {
                PostaviBazuPodataka(paket.TipBaze);
                bazaPodataka = true;
            }

            // Provera tipa datoteke
            var tipFile = GetTipLoada(paket.FileName);

            string text = ProcitajText(paket);

            var redovi = CSVParser(text);

            if (!ValidanBrojRedova(redovi))
            {
                string poruka = $"Nevalidan broj redova u fajlu {paket.FileName}. Ocekivan broj redova je: 23, 24, ili 25, ali {IzracunajBrojRedova(redovi)} je procitano.";
                rezultat = RezultatSlanjaIAudit(TipRezultata.Neuspesno, poruka, baza);
            }
            else
            {
                ukupanBrojRedova = IzracunajBrojRedova(redovi);
                rezultat = ObradaPaketa(redovi, paket, tipFile);
            }
            NoviImportedFile(paket.FileName, baza);
            redUCsv = 1;
            Console.WriteLine("Zavrsena obrada podataka za Fajl: " + paket.FileName);
            return rezultat;
        }

        public void PostaviBazuPodataka(TipBaze tip)
        {
            baza = Baza.BazaPodataka(tip);
            AzurirajBazu += AzurirajBazuPodatakaZaLoad;
        }

        private void AzurirajBazuPodatakaZaLoad(Load load)
        {
            baza.AzurirajLoad(load);
        }

        public TipLoada GetTipLoada(string imeFile)
        {
            if (imeFile.StartsWith("forecast"))
                return TipLoada.Forecast;
            if (imeFile.StartsWith("measured"))
            {
                fileId++;
                return TipLoada.Measured;
            } 
             throw new FormatException("Nepoznat tip loada.");
        }


        public string ProcitajText(PaketiZaSlanje paket)
        {
            paket.MS.Position = 0;
            StreamReader reader = new StreamReader(paket.MS);
            return reader.ReadToEnd();
        }

        public string[] CSVParser(string text)
        {
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public bool ValidanBrojRedova(string[] redovi)
        {
            return IzracunajBrojRedova(redovi) >= 23 && IzracunajBrojRedova(redovi) <= 25;
        }

        public int IzracunajBrojRedova(string[] redovi)
        {
            var brojRedova = redovi.Length;
            if (redovi[redovi.Length - 1].Equals(""))
            {
                brojRedova--;
            }
            if (redovi[0].Contains("TIME_STAMP"))
            {
                brojRedova--;
            }

            return brojRedova;
        }

        public RezultatSlanja RezultatSlanjaIAudit(TipRezultata tipRezultata, string poruka, IBaza bazapodataka)
        {
            RezultatSlanja rezultat = new RezultatSlanja { TipRezultata = tipRezultata, Poruka = poruka };
            NoviAudit(poruka, bazapodataka);
            return rezultat;
        }

        public void NoviAudit(string poruka, IBaza bazapodataka)
        {
            var audit = new Audit { Message = poruka, Timestamp = DateTime.Now };
            bazapodataka.DodajAudit(audit);
        }

        private RezultatSlanja ObradaPaketa(string[] redovi, PaketiZaSlanje paket, TipLoada tipLoada)
        {
            RezultatSlanja result = new RezultatSlanja();

            foreach (string red in redovi)
            {
                if (red.Equals("") || red.Contains("TIME_STAMP")) continue;

                var delovi = red.Split(',');

                if (delovi.Length != 2)
                {
                    string poruka = $"Red '{red}' u fajlu {paket.FileName} nije lepo formatiran.";
                    result = RezultatSlanjaIAudit(TipRezultata.Neuspesno, poruka, baza);
                    break;
                }

                var vreme = delovi[0];
                var potrosnja = double.Parse(delovi[1]);

                // Kreiranje ili ažuriranje objekta Load
                AzurirajIliNapraviNoviLoad(DateTime.Parse(vreme), potrosnja, tipLoada);
                ObradaReda(DateTime.Parse(vreme));
            }
            return result;
        }

        private void AzurirajIliNapraviNoviLoad(DateTime vreme, double potrosnja, TipLoada tipLoada)
        {
            Load load = baza.PreuzmiLoad(vreme) ?? new Load { Timestamp = vreme };

            if (tipLoada == TipLoada.Forecast)
            {
                load.ForecastValue = potrosnja;
            }
            else if (tipLoada == TipLoada.Measured)
            {
                load.MeasuredValue = potrosnja;
                load.ImportedFileId = fileId;
            }
            
            baza.AzurirajLoad(load);
        }

        private void ObradaReda(DateTime vreme)
        {
            if (StigliDoKrajaCsv())
            {
                bool IzracunatoOdstupanje = false;
                foreach (Load load in baza.PreuzmiSveLoadove(vreme))
                {
                    IzracunatoOdstupanje = load.IzracunajOdstupanje();
                    AzurirajBazu?.Invoke(load);
                }
                if (IzracunatoOdstupanje)
                    Console.WriteLine($"Baza podataka azurirana sa vrednostima izracunatog odstupanja za  {vreme:yyyy MM dd} Loadove");
            }
        }

        private bool StigliDoKrajaCsv()
        {
            return redUCsv++ == ukupanBrojRedova;
        }

        public void NoviImportedFile(string fileName, IBaza bazapodataka)
        {
            var importedFile = new ImportedFile { FileName = fileName };
            
            bazapodataka.DodajImportedFile(importedFile);
        }
    }
}
