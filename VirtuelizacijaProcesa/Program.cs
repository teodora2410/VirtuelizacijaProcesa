using Common;
using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;

namespace VirtuelizacijaProcesa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tipBaze = ConfigurationManager.AppSettings["BazaPodataka"];

            if (!Enum.TryParse(tipBaze, out TipBaze tip))
                tip = TipBaze.INMEMORY;

            Console.WriteLine($"{tip} tip baze se trenutno koristi.");

            ChannelFactory<ILoad> factory = new ChannelFactory<ILoad>("Servis");
            ILoad proxy = factory.CreateChannel();

            while (true)
            {
                Console.WriteLine("Unesi Putanju do svojih .csv Fajlova");
                var putanja = Console.ReadLine();


                if (!Directory.Exists(putanja))
                {
                    Console.WriteLine("Putanja ne postoji. Pokusaj ponovo!!");
                    continue;
                }

                SlanjePaketa sender = new SlanjePaketa(tip, putanja, proxy);
                sender.PosaljiPakete();

                Console.WriteLine("Unesi KRAJ za zaustavljanje programa ili bilo sta drugo da nastavis: ");
                if (Console.ReadLine().ToUpper().Equals("KRAJ"))
                    break;
            }
        }
    }
}
