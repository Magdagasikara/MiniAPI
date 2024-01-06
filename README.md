# MiniAPI

ER Diagram
![image](https://github.com/Magdagasikara/MiniAPI/assets/146171382/1e98c68b-fecf-42a9-8305-20a0cee1bb13)



- Hämta alla personer i systemet
app.MapGet("/person/{options}", PersonHandler.ListPersons);
OBS DUbbelkolla om tomt funkar nu!
http://localhost:5195/person/0
 
- Hämta alla intressen som är kopplade till en specifik person
app.MapGet("/person/{personId}/interest", PersonHandler.ListPersonsInterests);
http://localhost:5195/person/7/interest 

- Hämta alla länkar som är kopplade till en specifik person
app.MapGet("/person/{personId}/link", LinkHandler.ListPersonsLinks);
http://localhost:5195/person/7/link

- Koppla en person till ett nytt intresse
Kopplade till flera på en gång för att fylla på min Db: 
app.MapPost("/person/{personId}/interest", InterestHandler.AddPersonsInterests);
http://localhost:5195/person/7/interest
JSON:
[
	{
		"id": 9
	},
	{
		"id": 10
	},
	{
		"id": 14
	}
]
 
- Lägga in nya länkar för en specifik person och ett specifikt intresse
app.MapPost("/person/{personId}/interest/{interestId}/link", InterestHandler.CreatePersonsLinks);
http://localhost:5195/person/7/interest/9/link
JSON:
[
	{
		"link": "https://thebeach.se/"
	},
	{
		"link": "https://beachclub.nu/"
	}
]
 
**Extra utmaning (gör om du vill)**

- Ge möjlighet till den som anropar APIet och efterfrågar en person att direkt få ut alla intressen och alla länkar för den personen direkt i en hierarkisk JSON-fil
app.MapGet("/person/{personId}/interest/link", LinkHandler.ListPersonsInterestsAndLinks);
http://localhost:5195/person/7/interest/link

- Ge möjlighet för den som anropar APIet att filtrera vad den får ut, som en sökning. Exempelvis som jag skickar med “to” till hämtning av alla personer vill jag ha de som har ett “to” i namnet så som “tobias” eller “tomas”. Detta kan du sen skapa för alla anropen om du vill.
JUST NU DUMT utan query strings :( så här nu, samma anrop som första med [options]. ska fixas! 
t.ex. http://localhost:5195/person/ma

- Skapa paginering av anropen. När jag anropar exempelvis personer får jag kanske de första 100 personerna och får sen anropa ytterligare gånger för att få fler. Här kan det också vara snyggt att anropet avgör hur många personer jag får i ett anrop så jag kan välja att få säg 10st om jag bara vill ha det.
JUST NU DUMT utan query strings :( så här, samma anrop som första med [options]. ska fixas!
XXXXXXXXXXXXX
