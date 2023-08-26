using Common;
using System;
using System.Collections.Generic;

namespace BazaPodataka
{
    public interface IBaza
    {
        void DodajAudit(Audit audit);
        Load PreuzmiLoad(DateTime vreme);
        void AzurirajLoad(Load load);
        void DodajImportedFile(ImportedFile importedFile);
        List<Load> PreuzmiSveLoadove(DateTime vreme);
    }
}
