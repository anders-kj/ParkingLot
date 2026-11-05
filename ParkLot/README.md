# Parkera Bil 
```mermaid
sequenceDiagram
	ParkingAdmin->>ParkingService: Parkera bil ABC123!
    ParkingService->>ParkingZone: Antal Lediga Bilplatser?
	ParkingZone->>ParkingService: Det finns 9 lediga platser
	ParkingService->>ParkingSpotFinder: Hitta optimal parkeringsplats för bil
	ParkingSpotFinder->>ParkingZone:Lista lediga bilplatser!
	ParkingZone->>ParkingSpotFinder:Här är listan med lediga platser.
	ParkingSpotFinder->>ParkingSpotFinder:Sök igenom listan efter optimal plats. 
	Note right of ParkingSpotFinder: För varje ledig plats beräknas en poäng som baseras på hur tätt fordonen parkeras. 
	ParkingSpotFinder->>ParkingService: Parkeringsplats nummer 3 är bästa valet
	ParkingService->>ParkingService:Påbörja parkering för bil ABC123 på plats nummber 3
	ParkingService->>ParkingZone:Markera plats nummer 3 som upptagen!
	ParkingService->>ParkingAdmin: Bilen parkerad!



```

# Avsluta Parkering
```mermaid
sequenceDiagram
	ParkingAdmin->>ParkingService: Avsluta parkering för ABC123
    ParkingService->>ParkingService: Hitta aktiv parkering för ABC123
	ParkingService->>ParkingZone: Markera plats nummer 3 som ledig
	ParkingService->>ParkingService: Beräkna totalkostnad för parkering.
	ParkingService->>ParkingAdmin:Parkering avslutad.
	



```
