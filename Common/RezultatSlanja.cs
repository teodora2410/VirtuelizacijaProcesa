using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum TipRezultata
    {
        Uspesno,
        Neuspesno
    }
    public class RezultatSlanja
    {
        [DataMember]
        public string Poruka { get; set; }

        [DataMember]
        public TipRezultata TipRezultata { get; set; }

        public RezultatSlanja()
        {
            this.TipRezultata = TipRezultata.Uspesno;
        }
    }
}
