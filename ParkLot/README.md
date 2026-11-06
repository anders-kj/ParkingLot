
## **ParkingApp:** Hanterar interaktion med användare.
## **ParkingService** : Hanterar administration av aktiva parkeringar.
## **ParkingZone** : Hanterar  vilka platser som är lediga eller upptagna.
## **ParkingSpotFinder:** Har metoder för att hitta optimal plats för olika typer av fordon.

| Plats | Fordon |
| -------- | -------- |
| 1 | Bil qwe123 |
| 2 | Mc ert345 |
| 3 | _ |
| 4 | Bil rty567 |
| 5 | _ |
| 6 | _ |
| 7 | Bil fgh678 |
| 8| _|

***
# Parkera Bil 
```mermaid
sequenceDiagram
	ParkingApp->>ParkingService: Parkera bil ABC123!
    ParkingService->>ParkingZone: Antal Lediga Bilplatser?
	ParkingZone->>ParkingService: Det finns 4 lediga platser
	ParkingService->>ParkingSpotFinder: Hitta optimal parkeringsplats för bil
	ParkingSpotFinder->>ParkingZone:Lista lediga bilplatser!
	ParkingZone->>ParkingSpotFinder:Här är listan med lediga platser.
	ParkingSpotFinder->>ParkingSpotFinder:Sök igenom listan efter optimal plats. 
	Note right of ParkingSpotFinder: För varje ledig plats beräknas en poäng som baseras på hur tätt fordonet kan parkeras.
	ParkingSpotFinder->>ParkingService: Parkeringsplats nummer 3 är bästa valet
	ParkingService->>ParkingService:Påbörja parkering för bil ABC123 på plats nummber 3
	ParkingService->>ParkingZone:Markera plats nummer 3 som upptagen!



```

| Plats | Fordon |
| -------- | -------- |
| 1 | Bil qwe123 |
| 2 | Mc ert345 |
| 3 | **Bil abc123** |
| 4 | Bil rty567 |
| 5 | _ |
| 6 | _ |
| 7 | Bil fgh678 |
| 8| _|

# Avsluta Parkering
```mermaid
sequenceDiagram
	ParkingApp->>ParkingService: Avsluta parkering för ABC123
    ParkingService->>ParkingService: Hitta aktiv parkering för ABC123
	ParkingService->>ParkingZone: Markera plats nummer 3 som ledig
	ParkingService->>ParkingService: Beräkna totalkostnad för parkering.
	



```
| Plats | Fordon |
| -------- | -------- |
| 1 | Bil qwe123 |
| 2 | Mc ert345 |
| 3 | _ | 
| 4 | Bil rty567 |
| 5 | _ |
| 6 | _ |
| 7 | Bil fgh678 |
| 8| _|