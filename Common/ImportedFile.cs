using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ImportedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public static int sledeciId = 0;
        public ImportedFile()
        {
            Id = sledeciId;
        }
    }
}
