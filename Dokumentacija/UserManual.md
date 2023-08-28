# User Manual: Evidencija Prognozirane i Ostvarene Potrošnje Električne Energije

## Sadržaj
- [User Manual: Evidencija Prognozirane i Ostvarene Potrošnje Električne Energije](#user-manual-evidencija-prognozirane-i-ostvarene-potrošnje-električne-energije)
  - [Sadržaj](#sadržaj)
  - [Uvod](#uvod)
  - [Zahtevi](#zahtevi)
  - [Instalacija](#instalacija)
  - [Pokretanje Servisa](#pokretanje-servisa)
  - [Pokretanje Klijenta](#pokretanje-klijenta)
  - [Korišćenje Aplikacije](#korišćenje-aplikacije)

---

## Uvod

Ovaj User Manual će vas uputiti kako da postavite i koristite aplikaciju za evidenciju prognozirane i ostvarene potrošnje električne energije.

---

## Zahtevi

- Git
- Visual Studio
- .NET Framework

---

## Instalacija

1. **Kloniranje repozitorijuma sa GitHub-a**

    Otvorite terminal i unesite:
    ```bash
    git clone [https://github.com/teodora2410/VirtuelizacijaProcesa.git]
    ```

2. **Otvorite Solution u Visual Studiu**

    Nakon kloniranja, idite u folder gde je smeštena vaša aplikacija i otvorite `.sln` fajl pomoću Visual Studia.

---

## Pokretanje Servisa

1. U Visual Studio Solution Exploreru, desni klik na servis projekat, i izaberite `Set as StartUp Project`.

2. Pokrenite aplikaciju klikom na `Start` ili pritiskom na `F5`.

---

## Pokretanje Klijenta

1. Nakon što je servis pokrenut, idite nazad u Solution Explorer.

2. Desni klik na klijentski projekat, i izaberite `Debug`.

3. Pokrenite aplikaciju klikom na `Start New Instance`.

---

## Korišćenje Aplikacije

1. **Unos Putanje Do CSV Fajlova na Klijentskoj Strani**

    Nakon što je klijent pokrenut, sledite upute na konzolnom interfejsu. Unesite putanju do foldera koji sadrži `.csv` fajlove kada bude zatraženo.

    ```bash
    Unesite putanju do foldera: [Vaša Putanja]
    ```

2. **Pregled Podataka i Odstupanja**

    Klijentska aplikacija će vas obavestiti o statusu unosa i izvršenih proračuna.

---

Za dodatna pitanja i podršku, obratite se timu za podršku.
