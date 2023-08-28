# Evidencija Prognozirane i Ostvarene Potrošnje Električne Energije

## Uvod

### Ciljevi Projekta
- Uvoz podataka o prognoziranoj i ostvarenoj potrošnji iz CSV datoteka
- Proračun odstupanja između prognozirane i ostvarene potrošnje
- Cuvanje podataka u XML i In-Memory baze podataka

---

## Tehničke Specifikacije

### Arhitektura

Aplikacija koristi višeslojnu arhitekturu:

- **Baza podataka** (XML i In-Memory)
- **Servisni sloj**
- **Korisnički interfejs** (konzolna aplikacija)
- **Common projekat**

### Komunikacija

Komunikacija između klijentske aplikacije i servisa se vrši preko WCF-a.

---

## Korisnički Zahtevi

### Uvoz Podataka

Korisnik može da uvozi podatke iz CSV datoteka. Datoteke treba da sadrže podatke o potrošnji za svaki sat.

### Proračun Odstupanja

Aplikacija automatski proračunava odstupanja između prognozirane i ostvarene potrošnje.

---

## Model Podataka

### Klase

- **Load**: Id, Timestamp, ForecastValue, MeasuredValue, AbsolutePercentageDeviation, SquaredDeviation, ImportedFileId
- **ImportedFile**: Id, FileName
- **Audit**: Id, Timestamp, MessageType, Message

---

## Implementacija Baze Podataka

- **XML baza podataka**: Svaka tabela je implementirana kroz jednu XML datoteku.
- **In-Memory baza podataka**: Koristi se Dictionary za svaku tabelu.

---

## Tehnički i Implementacioni Zahtevi

- Korišćenje Dispose paterna za upravljanje memorijom
- Sva poslovna logika je na servisnoj strani
- Korišćenje MemoryStream-a za slanje datoteka
- Korišćenje Event-a i Delegate-a za ažuriranje baze podataka

---

## Dodatni Dokumenti

1. [User Manual](./UserManual.md)

