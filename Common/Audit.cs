using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum TipPoruke
    {
        Info,
        Warning,
        Error
    }
    public class Audit
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public TipPoruke MessageType { get; set; }
        public string Message { get; set; }

        public static int sledeciId = 0;
        public Audit()
        {
            Id = sledeciId;
        }
    }
}
