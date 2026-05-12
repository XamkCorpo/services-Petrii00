# Vaihe 2: Tietokanta — Teoriakysymykset

Vastaa alla oleviin kysymyksiin omin sanoin. Kirjoita vastauksesi kysymysten alle.

> **Vinkki:** Jos jokin kysymys tuntuu vaikealta, palaa lukemaan [Entity Framework Core](https://github.com/xamk-mire/Xamk-wiki/blob/main/C%23/fin/04-Advanced/WebAPI/Entity-Framework.md) -teoriamateriaalin.

---

## Osa 1: ORM ja EF Core

### Kysymys 1: Mikä on ORM?

Selitä omin sanoin mitä ORM tarkoittaa ja miksi se helpottaa kehittäjän työtä. Mitä joutuisit tekemään ilman ORM:ia?
**Vastaus:**
ORM tarkoittaa Object-Relational Mappingia, joka on tekniikka, joka mahdollistaa tietokantaoperaatioiden tekemisen olioiden avulla.
ORM-kirjastot, kuten Entity Framework Core, tarjoavat abstraktiokerroksen, joka muuntaa C#-oliot SQL-kyselyiksi ja päinvastoin.
Tämä helpottaa kehittäjän työtä, koska hän voi työskennellä tuttuina olevien olioiden kanssa ilman, että hänen tarvitsee kirjoittaa SQL-kyselyitä manuaalisesti



---

### Kysymys 2: DbContext

Mikä on `DbContext`:n rooli EF Core -sovelluksessa? Mitä tapahtuu jos unohdat rekisteröidä sen `Program.cs`:ssä?

**Vastaus:**
`DbContext` on EF Core -sovelluksessa keskeinen luokka, joka hallitsee tietokantayhteyksiä ja tarjoaa pääsyn tietokantatauluihin `DbSet`-olioiden kautta.
Se toimii siltana sovelluksen olioiden ja tietokannan välillä, mahdollistaen CRUD-operaatiot (Create, Read, Update, Delete) olioiden avulla.

---

### Kysymys 3: appsettings.json ja konfiguraatio

Mihin `appsettings.json`-tiedostoa käytetään ASP.NET Core -sovelluksessa? Selitä mitä seuraava rivi tekee `Program.cs`:ssä:

```csharp
builder.Configuration.GetConnectionString("DefaultConnection")
```

Miksi connection string määritellään `appsettings.json`-tiedostossa eikä kirjoiteta suoraan C#-koodiin?

**Vastaus:**
`appsettings.json`-tiedostoa käytetään ASP.NET Core -sovelluksessa sovelluksen konfiguraatiotietojen, kuten tietokantayhteyksien, API-avainten ja muiden asetusten tallentamiseen.
Rivi `builder.Configuration.GetConnectionString("DefaultConnection")` hakee `DefaultConnection`-nimisen tietokantayhteyden `appsettings.json`-tiedostosta.
Connection string määritellään `appsettings.json`-tiedostossa, koska se mahdollistaa konfiguraation muuttamisen ilman, että koodia tarvitsee muokata ja uudelleen kääntää.

---

### Kysymys 4: DbSet

Mitä `DbSet<Product> Products` tarkoittaa `AppDbContext`-luokassa? Miten se liittyy tietokantatauluun?

**Vastaus:**
`DbSet<Product> Products` tarkoittaa, että `AppDbContext`-luokalla on `Products`-niminen kokoelma, joka edustaa `Product`-entiteettiä tietokannassa.


---

## Osa 2: Migraatiot

### Kysymys 5: Migraation tarkoitus

Selitä migraatioiden tarkoitus. Mitä tapahtuu jos lisäät uuden kentän `Product`-luokkaan mutta et luo uutta migraatiota?

**Vastaus:**
Migraatioiden tarkoitus on hallita tietokannan skeeman muutoksia ajan myötä. Ne mahdollistavat tietokantataulujen, sarakkeiden ja muiden rakenteiden päivittämisen ilman, että vanhat tiedot menetetään.
Jos lisäät uuden kentän `Product`-luokkaan mutta et luo uutta migraatiota, tietokanta ei päivity vastaamaan uutta mallia, mikä voi johtaa virheisiin sovelluksessa.


---

### Kysymys 6: Migraatiokomennot

Selitä mitä seuraavat komennot tekevät:

- `dotnet ef migrations add InitialCreate`: Luo uuden migraation nimeltä `InitialCreate`, joka sisältää tietokannan luomiseen tarvittavat muutokset.
- `dotnet ef database update`: Päivittää tietokannan viimeisimmän migraation mukaiseksi, eli luo tai muuttaa tauluja ja sarakkeita tarpeen mukaan.
- `dotnet ef migrations remove`: Poistaa viimeisimmän migraation.

---

### Kysymys 7: Migraatioiden järjestys

Sinulla on jo `InitialCreate`-migraatio ajettuna. Lisäät nyt `Category`-entiteetin. Mitä vaiheita tarvitset tietokannan päivittämiseen?

**Vastaus:**
1. Lisää `Category`-entiteetti.
2. Luo uusi migraatio komennolla `dotnet ef migrations add AddCategory`.
3. Päivitä tietokanta komennolla `dotnet ef database update`.

---

## Osa 3: DTO:t ja Request-luokat

### Kysymys 8: Over-posting

Selitä mitä "over-posting" tarkoittaa. Miten DTO:n käyttö estää sen? Anna konkreettinen esimerkki mitä voisi tapahtua ilman DTO:ta.

**Vastaus:**
"Over-posting" tarkoittaa tilannetta, jossa käyttäjä lähettää enemmän tietoa kuin mitä sovellus odottaa, mikä voi johtaa tietoturvaongelmiin tai ei-toivottuihin muutoksiin tietokannassa.
DTO:n käyttö estää tämän rajoittamalla, mitä kenttiä voidaan vastaanottaa ja päivittää.

Esimerkki ilman DTO:ta: Jos `Product`-entiteetillä on `IsAdmin`-kenttä ja käyttäjä voi lähettää JSON-datan suoraan `Product`-entiteettiin, käyttäjä voisi muuttaa `IsAdmin`-arvon, vaikka sitä ei pitäisi sallia.

---

### Kysymys 9: Request vs. Response DTO

Tässä harjoituksessa luotiin erikseen `CreateProductRequest`, `UpdateProductRequest` ja `ProductResponse`. Miksi nämä ovat erillisiä luokkia eikä yksi yhteinen `ProductDto`?

**Vastaus:**
Erilliset DTO-luokat mahdollistavat tarkemman kontrollin siitä, mitä tietoja voidaan vastaanottaa ja palauttaa eri tilanteissa.
`CreateProductRequest` sisältää vain luomiseen tarvittavat kentät, `UpdateProductRequest` sisältää päivitykseen tarvittavat kentät ja `ProductResponse` sisältää ne kentät, jotka halutaan palauttaa asiakkaalle.
Tämä estää esimerkiksi ei-toivottujen kenttien muokkaamisen tai paljastamisen.

---

### Kysymys 10: Entiteetti vs. DTO

Mikä ero on `Product`-entiteetillä ja `ProductResponse`-DTO:lla? Missä tilanteessa ne alkaisivat erota toisistaan enemmän?

**Vastaus:**
`Product`-entiteetti edustaa tietokannan mallia ja sisältää kaikki tietokantaan tallennettavat kentät. `ProductResponse`-DTO puolestaan määrittelee, mitä tietoja palautetaan asiakkaalle API:n kautta.
Ne alkaisivat erota toisistaan enemmän, jos esimerkiksi tietokantaan lisätään kenttiä, joita ei haluta paljastaa asiakkaalle, tai jos halutaan yhdistää tietoja useista entiteeteistä yhteen DTO:hon.

---

## Osa 4: Extension methodit ja mapping

### Kysymys 11: Miksi muunnokset yhteen paikkaan?

Controllerissa muunnetaan DTO → entiteetti ja entiteetti → DTO. Miksi nämä muunnokset siirrettiin erilliseen `ProductMappings`-luokkaan sen sijaan, että ne tehtäisiin jokaisessa controller-metodissa erikseen?

**Vastaus:**
Muunnosten siirtäminen erilliseen `ProductMappings`-luokkaan parantaa koodin ylläpidettävyyttä ja uudelleenkäytettävyyttä.
Näin samaa muunnoslogiikkaa voidaan käyttää useissa paikoissa ilman toistoa, ja muutokset muunnoksiin voidaan tehdä yhdestä paikasta.

---

### Kysymys 12: Extension method

Selitä mitä `this`-avainsana tekee seuraavassa metodissa ja miten se vaikuttaa metodin kutsutapaan:

```csharp
public static ProductResponse ToResponse(this Product product)
```

Miten tätä metodia kutsutaan koodissa? Miltä kutsu näyttäisi ilman `this`-avainsanaa?

**Vastaus:**
Poistamalla `this`-avainsana, metodi olisi tavallinen staattinen metodi, joka vaatisi `Product`-olion välittämistä parametrina. Kutsutapa ilman `this`-avainsanaa näyttäisi tältä:
```csharp
ProductResponse response = ProductMappings.ToResponse(product);
```

---

### Kysymys 13: UpdateEntity-metodi

`UpdateEntity`-metodi ei palauta uutta oliota vaan muuttaa olemassa olevaa:

```csharp
public static void UpdateEntity(this UpdateProductRequest request, Product product)
```

Miksi tämä tehdään näin `Update`-endpointissa, vaikka `Create`-endpointissa luodaan kokonaan uusi `Product`-olio `ToEntity()`-metodilla?

**Vastaus:**
`UpdateEntity`-metodi muuttaa olemassa olevaa `Product`-oliota, koska päivitysoperaatiossa halutaan säilyttää entiteetin muut ominaisuudet, kuten ID ja mahdolliset luonti- tai päivityspäivämäärät, jotka eivät välttämättä kuulu `UpdateProductRequest`-DTO:hon.	


---
