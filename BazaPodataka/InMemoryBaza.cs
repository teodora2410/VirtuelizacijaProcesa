using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BazaPodataka
{
    public class InMemoryBaza : IBaza
    {
        public Dictionary<int, Load> Loads { get; set; } = new Dictionary<int, Load>();
        public Dictionary<int, ImportedFile> ImportedFiles { get; set; } = new Dictionary<int, ImportedFile>();
        public Dictionary<int, Audit> Audits { get; set; } = new Dictionary<int, Audit>();


        public void AzurirajLoad(Load load)
        {
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            Loads[load.Id] = load;
            Load.sledeciId++;
        }

        public void DodajAudit(Audit audit)
        {
            if (audit == null)
                throw new ArgumentNullException(nameof(audit));
            if (Audits.ContainsKey(audit.Id))
                throw new ArgumentException($"Audit sa ID {audit.Id} vec postoji.");
            Audits.Add(audit.Id, audit);
            Audit.sledeciId++;
        }

        public void DodajImportedFile(ImportedFile importedFile)
        {
            if (importedFile == null)
                throw new ArgumentNullException(nameof(importedFile));
            if (ImportedFiles.ContainsKey(importedFile.Id))
                throw new ArgumentException($"Imported file sa ID {importedFile.Id} vec postoji.");
            ImportedFiles.Add(importedFile.Id, importedFile);
            ImportedFile.sledeciId++;
        }

        public Load PreuzmiLoad(DateTime vreme)
        {
            return Loads.Values.FirstOrDefault(load => load.Timestamp == vreme);
        }

        public List<Load> PreuzmiSveLoadove(DateTime vreme)
        {
            return Loads.Values.Where(load => load.Timestamp.Date == vreme.Date).ToList();
        }
    }
}
