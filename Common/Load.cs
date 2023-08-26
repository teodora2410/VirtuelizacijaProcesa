using System;
using System.Configuration;

namespace Common
{
    public class Load
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double ForecastValue { get; set; }
        public double MeasuredValue { get; set; }
        public double AbsolutePercentageDeviation { get; set; }
        public double SquaredDeviation { get; set; }
        public int ImportedFileId { get; set; }

        public static int sledeciId = 0;

        public Load()
        {
            Id = sledeciId;
        }

        public bool IzracunajOdstupanje()
        {
            if (MeasuredValue == 0 || ForecastValue == 0)
            {
                return false;
            }

            // Kljuc u App.config "Odstupanje"
            var odstupanje = ConfigurationManager.AppSettings["Odstupanje"];

            switch (odstupanje)
            {
                case "Apsolutno":
                    AbsolutePercentageDeviation = Math.Abs(MeasuredValue - ForecastValue) / MeasuredValue * 100;
                    return true;
                case "Kvadratno":
                    SquaredDeviation = Math.Pow((MeasuredValue - ForecastValue) / MeasuredValue, 2);
                    return true;
                default:
                    throw new InvalidOperationException($"Nepoznato odstupanje: {odstupanje}");
            }
        }
    }
}
