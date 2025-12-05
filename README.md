# GitHub Copilotin peruskäyttö ja Agent Mode

Tämä opas on tarkoitettu aloittelevalle GitHub Copilotin käyttäjälle, jolla on jo kokemusta ohjelmoinnista. Esimerkeissä käytetään C#-ohjelmointikieltä, mutta kuvattuja menetelmiä voi soveltaa myös muilla ohjelmointikielillä.

## Sisältö

Tässä oppaassa kerrotaan, miten tekoälypohjaista GitHub Copilot -koodiavustinta ja sen Agent Mode -toimintoa käytetään Visual Studio Codessa. Esimerkkisovellukset tehdään C#-ohjelmointikielellä.

Oppaassa kerrotaan seuraavista Copilotin käyttöön liittyvistä asioista:
- Miten automaattinen koodintäydennys toimii
- Kehotteiden antaminen kommenttien ja Inline Chatin kautta
- Chat-ikkunan käyttö koodin selittämiseen
- Agent Moden käyttö projektien perustamisessa ja koodin refaktoroinnissa

## GitHub Copilot

GitHub Copilot on tekoälyyn pohjautuva ohjelmointityökalu, joka osaa luoda ja täydentää ohjelmakoodia. Copilotille kuvataan ohjelmointitehtävä luonnollisella kielellä ja se tuottaa ratkaisun annettuun tehtävään valitulla ohjelmointikielellä. Haluttu uusi ohjelman toiminto voidaan kuvata esimerkiksi Inline Chat -ikkunassa tai kuvaus voidaan kirjoittaa ohjelmakoodin kommentteihin. Copilot osaa myös ehdottaa seuraavia koodirivejä samalla, kun ohjelmoija kirjoittaa ohjelmakoodia. Copilot osaa generoida myös suurempia koodikokonaisuuksia, jotka jakaantuvat useisiin ohjelmistomoduuleihin. GitHub Copilot toimii muun muassa Visual Studio Codessa (VS Code) ja Visual Studiossa.

Copilotia markkinoidaan tekoälypohjaisena pariohjelmoijana, jolta voi kysyä ohjeita ohjelmointityön aikana. Copilotia voi pyytää myös selittämään annettua ohjelmakoodia tai virheilmoituksia. Virhetilanteissa Copilot selittää ongelman ja antaa korjausehdotuksen. Copilot on myös hyödyllinen yksikkötestien laatimisessa. Copilot toimii parhaiten, kun sille on annettu tietoa kehitettävän ohjelman kontekstista. Käytännössä tämä tarkoittaa sitä, että Copilotille kerrotaan, mitä ohjelmakoodia sen tulee huomioida koodiehdotuksia varten. 

## GitHub Copilot Agent Mode

GitHub julkaisu marraskuussa 2024 Edits-toiminnon GitHub Copilotiin. Edits-toiminto muistuttaa Chat-ikkunaa, mutta Copilot Edits osaa tehdä ehdottamansa muutokset suoraan ohjelmakooditiedostoihin. Copilot Edits -toiminnosta on kerrottu täällä [text](https://github.com/SeAMKedu/CopilotTutorial).

Alkuvuodesta 2025 esitelty Agent mode laajentaa Edits Modea. Edits Mode keskittyy yksittäisiin koodimuutoksiin, kun taas Agent Moden avulla voidaan tehdä suurempia muutoksia ohjelmakooditiedostoihin vaiheittain. 

Agent Mode tuo "agenttimaisen" toiminnallisuuden VS Codeen. Koodiehdotusten lisäksi se osaa tekemään laajempia koodaustehtäviä vaiheittain. Agentti pystyy lukemaan tiedostoja ja muokkaamaan projektin tiedostoja sekä ajamaan komentoja terminaalissa. Agentti toimii vuorovaikutteisesti ohjelmoijan kanssa. Työn kulku menee siten, että ohjelmoija antaa Colpilotille tavoitteen ja agentti laatii suunnitelman ja vie prosessia eteenpäin. Agentti etenee pienin askelin ja antaa statuspäivityksen muutaman toimenpiteen välein. Agentti pyytää käyttäjää vahvistamaan muutosehdotukset ja komentojen ajon terminaalissa. 

## GitHub Copilotin asentaminen VS Codeen

GitHub Copilotia varten tarvitaan Visual Studio Code ja GitHub-tili. Asennusohjeet löytyvät sivulta [Installing the GitHub Copilot extension in your environment](https://docs.github.com/en/copilot/managing-copilot/configure-personal-settings/installing-the-github-copilot-extension-in-your-environment).

Varmista ensin, että sinulla on pääsy GitHub Copilotiin. Ohjeet ovat sivulla [Getting access to Copilot](https://docs.github.com/en/copilot/about-github-copilot/what-is-github-copilot#getting-access-to-copilot). 

Jos olet opiskelija tai opettaja, älä avaa ilmaista kokeilujaksoa. Opiskelijat saavat käyttää GitHub Copilotia yleensä ilmaiseksi. Hanki pääsy GitHub Copilotiin näiden ohjeiden avulla: [Getting free access to Copilot as a student, teacher, or maintainer](https://docs.github.com/en/copilot/managing-copilot/managing-copilot-as-an-individual-subscriber/managing-your-copilot-subscription/getting-free-access-to-copilot-as-a-student-teacher-or-maintainer).

Asenna seuraavaksi GitHub Copilot -laajennus Visual Studio Codeen: [GitHub Copilot extension](https://docs.github.com/en/copilot/managing-copilot/configure-personal-settings/installing-the-github-copilot-extension-in-your-environment).

## Esimerkkisovellus C#-ohjelmointikielellä

Tutustutaan Copilotin käyttöön tekemällä pieni esimerkkisovellus Copilotin avulla. Esimerkkisovellus käsittelee Suomen kuntien tietoja.

Harjoituksessa perehdytään ensin automaattiseen koodintäydennykseen sekä kehotteiden antamiseen Inline Chatin ja kommenttien avulla. Tämän jälkeen kokeillaan Copilotia koodin selittämiseen. Lopuksi tutustutaan Copilot Agent Modeen, jonka avulla voi tehdä useampaan tiedostoon kohdistuvia muutoksia.

### Tarvittavat ohjelmistot

Asenna seuraavat ohjelmistot, ellei sinulla ole jo niitä:

- [.NET SDK](https://dotnet.microsoft.com/download).
- [VS Code](https://code.visualstudio.com/download)
- C# Extension. Asenna tämä VS Coden laajennoksissa (Extensions).

### Projektin perustaminen

Tämä harjoitus tehdään pääosin Visual Studio Code -editorilla. Projekti kannattaa kuitenkin luoda täydellä Visual Studio -versiolla, että projektiin syntyy debuggausta varten tarvittavat tiedostot. Copilotin käyttö käytto on kuitenkin sujuvampaa VS Codessa.

![Visual Studio](images/visualstudio.png)

Avaa Visual Studio
- Create a new project
- Valitse Console App (Ei .NET Framework)
- Valitse Do not use top-level statements
- Paina Create
- Testaa ohjelma ajamalla se (Debug - Start Without Debugging)

Sulje Visual Studio nyt. Navigoi komentorivillä siihen hakemistoon, mihin projekti syntyi.

> [!NOTE]
> Jos et halua kuitenkaan käyttää täyttä Visual Studiota, voit tehdä projektin C#-konsolisovellusta varten terminaalista. Tämä on siis vaihtoehtoinen tapa luoda projekti:
> ```
> dotnet new console -n ReadMunicipalityData
> ```
> Komento tekee hakemiston ReadMunicipalityData ja sen alle projektitiedoston ReadMunicipalityData.csproj ja ohjelmakooditiedoston Program.cs.

> Siirry seuraavaksi syntyneeseen projektihakemistoon.
> ```
> cd ReadMunicipalityData
> ```

> Voit myös kääntää ohjelman tässä vaiheessa. Tämä ei ole > välttämätöntä, sillä ohjelma käännetään ajettaessa, jos siihen on tehty muutoksia edellisen käännöksen jälkeen.
> ```
> dotnet build
> ```

Aja seuraavaksi ohjelma komentoriviltä.
```
dotnet run
```
Käynnistä VS Code näin. VS Code avautuu samaan hakemistoon, mistä käynnistys tehtiin.
```
code .
```

### Inline Chat

Tehdään seuraavaksi ohjelma, joka lukee tiedoston [kunnat2024.csv](kunnat2024.csv). Tiedoston sarakkeet ovat ID, nimi ja asukasluku. Tiedot on erotettu pilkulla.

Alkuperäinen data löytyy [kuntaliiton sivulta](https://www.kuntaliitto.fi/kuntaliitto/tietotuotteet-ja-palvelut/kaupunkien-ja-kuntien-lukumaarat-ja-vaestotiedot).

```
20;Akaa;16387
5;Alajärvi;9078
9;Alavieska;2410
10;Alavus;10780
16;Asikkala;7889
18;Askola;4651
```

Pyydetään Copilotia tekemään ensin pelkkä tiedoston lukeminen. Annetaan kehote Inline Chatistä. Inline Chatin saa päälle painamalla Ctrl+I, kuten VS Code ehdottaa.

Annetaan Inline Chatille prmpti: Make a program which reads file kunnat2024.csv, The program makes an object of class Municipality of each row in the file. The objects are stored in a a list.

KUVA

Inline Chat aukeaa koodieditoriin tiedostokohtaisesti. Tekoälyn avulla käytävä keskustelu kohdistuu siis avoinna olevaan tiedostoon.

Inline Chatin lisäksi keskustelua voi käydä myös Chat-ikkunassa, joka avautuu ruudun oikealle puolelle omaan ikkunaansa. Chat-ikkunan käytöstä kerrotaan tässä oppaassa myöhemmin.