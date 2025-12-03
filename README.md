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

