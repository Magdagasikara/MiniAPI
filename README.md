# MiniAPI


- H�mta alla personer i systemet
app.MapGet("/person/{options}", PersonHandler.ListPersons);
OBS DUbbelkolla om tomt funkar nu!
http://localhost:5195/person/0
 
- H�mta alla intressen som �r kopplade till en specifik person
app.MapGet("/person/{personId}/interest", PersonHandler.ListPersonsInterests);
http://localhost:5195/person/7/interest 

- H�mta alla l�nkar som �r kopplade till en specifik person
app.MapGet("/person/{personId}/link", LinkHandler.ListPersonsLinks);
http://localhost:5195/person/7/link

- Koppla en person till ett nytt intresse
Kopplade till flera p� en g�ng f�r att fylla p� min Db: 
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
 
- L�gga in nya l�nkar f�r en specifik person och ett specifikt intresse
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
 
**Extra utmaning (g�r om du vill)**

- Ge m�jlighet till den som anropar APIet och efterfr�gar en person att direkt f� ut alla intressen och alla l�nkar f�r den personen direkt i en hierarkisk JSON-fil
app.MapGet("/person/{personId}/interest/link", LinkHandler.ListPersonsInterestsAndLinks);
http://localhost:5195/person/7/interest/link

- Ge m�jlighet f�r den som anropar APIet att filtrera vad den f�r ut, som en s�kning. Exempelvis som jag skickar med �to� till h�mtning av alla personer vill jag ha de som har ett �to� i namnet s� som �tobias� eller �tomas�. Detta kan du sen skapa f�r alla anropen om du vill.
JUST NU DUMT utan query strings :( s� h�r nu, samma anrop som f�rsta med [options]. ska fixas! 
t.ex. http://localhost:5195/person/ma

- Skapa paginering av anropen. N�r jag anropar exempelvis personer f�r jag kanske de f�rsta 100 personerna och f�r sen anropa ytterligare g�nger f�r att f� fler. H�r kan det ocks� vara snyggt att anropet avg�r hur m�nga personer jag f�r i ett anrop s� jag kan v�lja att f� s�g 10st om jag bara vill ha det.
JUST NU DUMT utan query strings :( s� h�r, samma anrop som f�rsta med [options]. ska fixas!
XXXXXXXXXXXXX