# RCS Beweegroute

### 17-04-2020

## API key voor het weergeven van Maps 

Als eerste komt de Maps API key. iOS heeft geen API key nodig tijdens het testen, maar heeft er wel een nodig wanneer de 
applicatie gedeployed wordt. De API key voor Android werkte alleen tijdens het ontwikkelen van de applicatie. Er is een
vergelijking gedaan naar de verschillende opties voor map API keys vanwege de eventuele bijkomende kosten. Deze is te vinden in het bestand "Maps API onderzoek". Maar als RCS geen interesse heeft om de applicatie uit te brengen voor Android, dan is dit natuurlijk niet nodig. Voor iOS geldt dat een een developers account bij Apple nodig is om een map API key aan te vragen. Meer informatie
is te vinden in deze link: https://developer.apple.com/documentation/mapkitjs/creating_a_maps_identifier_and_a_private_key , maar 
de Robert Coppes stichting heeft op dit moment nog geen developers account.


## Uitbrengen applicatie

Als 2e geldt hoe ze de applicatie gaan uitbrengen. Er is onderzoek gedaan naar het uitbrengen in de Apple App Store en de 
Google Play Store. Dit is te vinden in het bestand "Onderzoek Appstore en google store". Dit is natuurlijk niet vereist,
maar dit maakt de app wel makkelijker te vinden. Dit zou dus verder besproken worden besprokenmet de Robert Coppes Stichting. Voor
het uitbrengen van de applicatie bij Apple is een developers account vereist, wat, zoals eerder al gezegd is, bij tijden van schrijven
nog niet beschikbaar is voor de Robert Coppes Stichting.

Private repositorie

Hoe de Robert Coppes Stichting met de code en alle omliggende documentatie om wilt gaan is voor ons niet duidelijk. Als RCS het niet open wil laten staan voor iedereen die het wilt zien, raden wij ze aan om de repositorie privé te maken. Er zou eventueel ook
een licentie bijgezet kunnen worden, maar dit zal in overleg met de Robert Coppes Stichting gebeuren.


## Aanbevelingen voor de code

Aangezien er een tijdslimiet zat aan de tijd van het ontwikkelen van de code, komen hier enkele aanbevelingen voor in de toekomst
voor het verbeteren van de applicatie:

1. Geofencing, voor het beter detecteren waar de gebruiker van de applicatie is tijdens het lopen van een route en of deze 
nog op de route is.
2. Betere text-to-speech, nu is de text-to-speech op een basisniveau en spreekt deze door elkaar als je te snel door de menu's heen gaat. Kijk of het mogelijk is om het overgaan naar een nieuwe pagina onmogelijk te maken terwijl gesproken wordt, of de text-to-speech te stoppen als je naar een nieuwe pagina gaat.
3. Implementeren dat de backbutton niet werkt bij de menu's die alleen voor de slechtzienden zijn. Zo kan een route niet per ongeluk 
gestopt worden en dit voorkomt ook dat text-to-speech daar elkaar gaat praten.
4. De mogelijkheid om routes daar te laten lopen op de achtergrond, bijvoorbeeld als het scherm uit is.
5. De functionaliteit van het lopen van een route verbeteren. Deze werkt nu alleen als de gebruiker ook precies langs ieder coördinaat
loopt.
6. De code opruimen. Er is waarschijnlijk vrij veel dubbele code in de menu's die bedoeld zijn voor de begeleiders. Kijk bijvoorbeeld of er menu's samengevoegd kunnen worden.


### Verdere aandachtspunten

Ook zijn er nog een aantal aandachtspunten waar we op willen wijzen:
1. We kunnen iedereen aanbevelen om gebruik te maken van de Github extensie van Visual Studio. Dat zorgt ervoor dat je de code direct kunt aanpassen in Visual Studio en met een simpele commit en push de code ook direct op Github hebt.
2. Het is verstanding als iedere developer die in de toekomst aan de code werkt in de Test branch werkt. Alles dat in de master branch staat werkt op dit moment dus is het verstandig om daar vanaf te blijven.
3. Het is handig om deze README up-to-date te houden als er dingen veranderen, bijvoorbeeld als aanbevelingen zijn omgezet in werkende functionaliteiten.

Bij verdere vragen of onduidelijkheden in de code stuur een e-mail naar silasvaessens@gmail.com, maar we hopen dat de bijgeleverde documentatie en comments in de code duidelijk genoeg zijn.
