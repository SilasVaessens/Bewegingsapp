# Bewegingsapp
code bewegingsapp

17-04-2020

API key voor weergeven Maps

Als eerste komt het de Maps API key. iOS had geen nodig tijdens het testen, maar heeft er wel een nodig voor als de 
applicatie gedeployed heeft, terwijl de API key voor Android alleen werkte tijdens het ontwikkelen van applicatie. Als
RCS geen interesse heeft in de applicatie uitbrengen voor Android, dan is dit natuurlijk niet nodig, maar er is een
vergelijking gedaan naar de verschillende opties vanwege de eventuele bijkomende kosten, deze is te vinden in het bestand
"Maps API onderzoek". Voor iOS geldt dat een een developers account bij Apple nodig is om een aan te vragen, maar meer informatie
is te vinden in deze link: https://developer.apple.com/documentation/mapkitjs/creating_a_maps_identifier_and_a_private_key , maar 
de Robert Coppes stichting heeft nog geen developers account.


Uitbrengen applicatie

Als 2e geldt hoe ze de applicatie gaan uitbrengen. Er is onderzoek gedaan naar het uitbrengen in de Apple App Store en de 
Google Play Store, dit is overigens te vinden in het bestand "Onderzoek Appstore en google store". Dit is natuurlijk niet vereist,
maar dit maakt de app wel het makkelijkst te vinden, dit moet ook verder besproken worden met de Robert Coppes Stichting. Voor
het uitbrengen van de applicatie bij Apple is een developers account vereist, wat, zoals eerder al gezegd is, bij tijden van schrijven
nog niet beschikbaar is voor de Robert Coppes Stichting.

Private repositorie

Ik weet niet hoe de Robert Coppes Stichting met de code en alle omliggende documentatie om wilt gaan, maar als ze het niet open
willen laten staan voor iedereen die het wilt zien, raadt ik ze het aan om de repositorie privé te maken. Daarnaast misschien er ook
een licentie bij zetten, maar dit allemaal is in overleg met de Robert Coppes Stichting.


Aanbevelingen voor de code

Aangezien er een tijdslimiet zat aan de tijd van het ontwikkelen van de code, komen hier enkele aanbevelingen voor in de toekomst
voor het verbeteren van de applicatie:

1. Geofencing, voor het beter detecteren waar de gebruiker van de applicatie is tijdens het lopen van een route en of deze 
nog op de route is.
2. Betere text-to-speech, nu is de text-to-speech heel erg basis en spreekt door elkaar als je te snel door de menu's heen gaat,
kijk of het mogelijk is of om het overgaan naar een nieuwe pagina onmogelijk te maken terwijl gesproken wordt, of de text-to-speech
te cancellen als je naar een nieuwe pagina gaat.
3. Implementeren dat de backbutton niet werkt bij de menu's die alleen voor de slechtzienden zijn, zo kan een route niet per ongeluk 
gestopt worden, voorkomt ook dat text-to-speech daar elkaar gaat.
4. De mogelijkheid om routes daar te laten lopen op de achtergrond, bijvoorbeeld als het scherm uit is.
5. De functionaliteit van het lopen van een route verbeteren, werkt nu alleen als de gebruiker ook precies langs ieder coördinaat
loopt.
6. De code opruimen, heb waarschijnlijk vrij veel dubbele code in de menu's die bedoeld zijn voor de gebruiker, kijk of er menu's 
samengevoegd kunnen worden

Er zijn natuurlijk nog meer dingen te verbeteren, maar dit zijn de belangrijkste punten. Daarnaast wil ik iedereen nog aanbevelen
om gebruik te maken van de Github extensie van Visual Studio, dat maakt dat je de code direct kunt aanpassen in Visual Studio en
met een simpele commit en push de code ook direct op Github hebt. Daarnaast verzoek ik iedere volgende developer om in de Test branch
te werken, want alles in de master branch werkt, dus vanaf blijven. Daarnaast ook een verzoek om deze README up-to-date te houden
als er dingen veranderen, bijvoorbeeld als aanbevelingen zijn omgezet in werkende functionaliteiten. Bij verdere vragen of 
onduidelijkheden in de code stuur een e-mail naar silasvaessens@gmail.com, maar ik hoop dat de bijgeleverde documentatie en comments
in de code duidelijk genoeg zijn.
