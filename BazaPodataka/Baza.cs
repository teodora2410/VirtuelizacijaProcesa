using Common;
using System;

namespace BazaPodataka
{
    public static class Baza
    {
        public static IBaza CreateDatabase(TipBaze tipBaze)
        {
            switch (tipBaze)
            {
                case TipBaze.XML:
                    return new XMLBaza();
                case TipBaze.INMEMORY:
                    return new InMemoryBaza();
                default:
                    throw new InvalidOperationException($"Ovaj tip baze ne postoji: {tipBaze}");
            }
        }
    }
}
