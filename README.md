# MiniAPI


## ER diagram

![image](https://github.com/Magdagasikara/MiniAPI/assets/146171382/e051c51e-6da5-4553-9464-d3d44a079dcd)


## Anrop

- Hämta alla personer i systemet:

  GET http://localhost:5195/person
 
- Hämta alla intressen som är kopplade till en specifik person

  GET http://localhost:5195/person/7/interest 

- Hämta alla länkar som är kopplade till en specifik person

  GET http://localhost:5195/person/7/link

- Koppla en person till ett nytt intresse (kopplar personen till flera intressen på en gång för att fylla på min Db)

  POST http://localhost:5195/person/7/interest

  Json: `[ { "id": 9 }, { "id": 10 }, { "id": 14 } ]`
 
- Lägga in nya länkar för en specifik person och ett specifikt intresse

  POST http://localhost:5195/person/7/interest/9/link

  Json: `[ { "link": "https://thebeach.se/" }, { "link": "https://beachclub.nu/" } ]`

 
**Extra utmaning (gör om du vill)**

- Ge möjlighet till den som anropar APIet och efterfrågar en person att direkt få ut alla intressen och alla länkar för den personen direkt i en hierarkisk JSON-fil

  GET http://localhost:5195/person/7/interest/link

- Ge möjlighet för den som anropar APIet att filtrera vad den får ut, som en sökning. Exempelvis som jag skickar med “to” till hämtning av alla personer vill jag ha de som har ett “to” i namnet så som “tobias” eller “tomas”. Detta kan du sen skapa för alla anropen om du vill.

  GET http://localhost:5195/person/?firstName=ma&lastName=k

  GET http://localhost:5195/person/?amountPerPage=3&pageNumber=1&firstName=j&lastName

- Skapa paginering av anropen. När jag anropar exempelvis personer får jag kanske de första 100 personerna och får sen anropa ytterligare gånger för att få fler. Här kan det också vara snyggt att anropet avgör hur många personer jag får i ett anrop så jag kan välja att få säg 10st om jag bara vill ha det.

  GET http://localhost:5195/person/?amountPerPage=3&pageNumber=2

  GET http://localhost:5195/person/?amountPerPage=3&pageNumber=1&firstName=j&lastName


## UML, klassdiagram

![image](https://github.com/Magdagasikara/MiniAPI/assets/146171382/969084b4-09f0-4417-8cd0-dc901e16a0b9)
