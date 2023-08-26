using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BazaPodataka
{
    public class XMLBaza : IBaza
    {
        private readonly string loadPutanja = "Load.xml";
        private readonly string importedPutanja = "ImportedFile.xml";
        private readonly string auditPutanja = "Audit.xml";

        public void AzurirajLoad(Load load)
        {
            var podaci = Deserializacija<List<Load>>(loadPutanja) ?? new List<Load>();

            var postojeciID = podaci.FindIndex(l => l.Id == load.Id);
            if (postojeciID == -1)
            {
                podaci.Add(load);
                Load.sledeciId++;

            }
            else podaci[postojeciID] = load;
            Serializacija(podaci, loadPutanja);
        }

        public void DodajAudit(Audit audit)
        {
            var audits = Deserializacija<List<Audit>>(auditPutanja) ?? new List<Audit>();
            audits.Add(audit);
            Audit.sledeciId++;
            Serializacija(audits, auditPutanja);
        }

        public void DodajImportedFile(ImportedFile importedFile)
        {
            var importedFiles = Deserializacija<List<ImportedFile>>(importedPutanja) ?? new List<ImportedFile>();
            importedFiles.Add(importedFile);
            ImportedFile.sledeciId++;
            Serializacija(importedFiles, importedPutanja);
        }

        public Load PreuzmiLoad(DateTime vreme)
        {
            var podaci = Deserializacija<List<Load>>(loadPutanja) ?? new List<Load>();
            return podaci.FirstOrDefault(l => l.Timestamp == vreme);
        }

        public List<Load> PreuzmiSveLoadove(DateTime vreme)
        {

            var podaci = Deserializacija<List<Load>>(loadPutanja) ?? new List<Load>();
            return podaci.Where(load => load.Timestamp.Date == vreme.Date).ToList();
        }
        private T Deserializacija<T>(string putanja)
        {
            if (!File.Exists(putanja))
                return default;

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(putanja, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        private void Serializacija<T>(T podaci, string putanja)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(putanja, FileMode.Create))
            {
                serializer.Serialize(stream, podaci);
            }
        }
    }
}
