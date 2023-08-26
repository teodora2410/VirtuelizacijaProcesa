using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum TipBaze
    {
        XML,
        INMEMORY
    }

    [DataContract]
    public class PaketiZaSlanje : IDisposable
    {
        [DataMember]
        public MemoryStream MS { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public TipBaze TipBaze { get; set; }

        public PaketiZaSlanje(MemoryStream ms, string fileName, TipBaze tipBaze)
        {
            this.MS = ms;
            this.FileName = fileName;
            this.TipBaze = tipBaze;
        }

        public void Dispose()
        {
            if (MS == null)
                return;
            try
            {
                MS.Dispose();
                MS.Close();
                MS = null;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Neuspesno ciscenje!");
            };
        }
    }
}
