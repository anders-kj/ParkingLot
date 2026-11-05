namespace ParkLot;

internal class ParkingService
{
    private const decimal COST_PER_MINUTE = 1.5M;
    private ParkingZone parkingZone;
    private List<Parking> activeParkings;
    public int TotalNumberOfParkingSpots { get; }

    public ParkingService(int nofSpots)
    {
        parkingZone = new ParkingZone(nofSpots);
        activeParkings = new List<Parking>();
        TotalNumberOfParkingSpots = nofSpots;
    }

    public void Park(MotorCycle bike)
    {
        if (parkingZone.NofAvailableHalfSpots > 0)
        {
            int spotNumber = ParkingSpotFinder.FindHalfSpot(parkingZone);
            AddNewParking(bike, spotNumber);
        }
        else
        {
            Console.WriteLine($"---> Parkering Misslyckades för {bike.ToString()}. Välkommen Åter!");
        }
    }

    public void Park(Car car)
    {
        if (parkingZone.NofAvailableFullSpots > 0)
        {
            int spotNumber = ParkingSpotFinder.FindFullSpot(parkingZone);

            AddNewParking(car, spotNumber);
        }
        else
        {
            Console.WriteLine($"---> Parkering Misslyckades för {car.ToString()}. Välkommen Åter!");
        }
    }

    public void Park(Bus bus)
    {
        if (parkingZone.NofAvailableDoubleSpots > 0)
        {
            int spotNumber = ParkingSpotFinder.FindDoubleSpot(parkingZone);
            AddNewParking(bus, spotNumber);
        }
        else
        {
            Console.WriteLine($"---> Parkering Misslyckades för  {bus.ToString()}. Välkommen Åter!");
        }
    }

    public void Park(MotorCycle mc, int spotNumber)
    {
        if (parkingZone.HasAvailableHalfParking(spotNumber))
        {
            AddNewParking(mc, spotNumber);
        }
        else
        {
            Console.WriteLine($"ParkingSpot {spotNumber} can not fit {mc}");
        }
    }

    public void Park(Car car, int spotNumber)
    {
        if (parkingZone.HasAvailableFullParking(spotNumber))
        {
            AddNewParking(car, spotNumber);
        }
        else
        {
            Console.WriteLine($"ParkingSpot {spotNumber} can not fit {car}");
        }
    }

    public void Park(Bus bus, int spotNumber)
    {
        if (parkingZone.HasAvailableDoubleParking(spotNumber))
        {
            AddNewParking(bus, spotNumber);
        }
        else
        {
            Console.WriteLine($"ParkingSpot {spotNumber} can not fit {bus}");
        }
    }

    private void AddNewParking(Vehicle v, int spotIndex)
    {
        Parking p = new Parking(v, spotIndex);
        activeParkings.Add(p);

        switch (v)
        {
            case MotorCycle:
                parkingZone.ParkHalfSpot(spotIndex);
                break;
            case Car:
                parkingZone.ParkFullSpot(spotIndex);
                break;
            case Bus:
                parkingZone.ParkDoubleSpot(spotIndex);
                break;
        }
        Console.WriteLine($"Startar parkering för {v.ToString()} på plats {spotIndex}");
    }

    private Parking GetActiveParkingFor(string regNumber)
    {
        if (regNumber == null) return null;
        foreach (var p in activeParkings)
        {
            if (p.ContainVehicleWith(regNumber))
            {
                return p;
            }
        }
        return null;
    }

    public bool CheckOut(string regNumber)
    {
        bool result = false;
        Parking p = GetActiveParkingFor(regNumber);
        if (p != null)
        {
            Console.WriteLine($"Avslutar parkering för {p.ToString()} Total Kostnad : {CalculateCost(p):F2} kr");
            CheckOut(p);
            result = true;
        }
        else
        {
            Console.WriteLine($"Hittar ej Registreringsnummer {regNumber} för Aktiva Parkerade fordon.");
        }
        return result;
    }

    private void CheckOut(Parking p)
    {
        activeParkings.Remove(p);

        switch (p.Vehicle)
        {
            case MotorCycle:
                parkingZone.ReleaseHalfSpot(p.ParkingSpotIndex);
                break;
            case Car:
                parkingZone.ReleaseFullSpot(p.ParkingSpotIndex);
                break;
            case Bus:
                parkingZone.ReleaseDoubleSpot(p.ParkingSpotIndex);
                break;
        }
    }

    private decimal CalculateCost(Parking p)
    {
        decimal result = ((decimal)p.ParkingTimeSeconds / (decimal)60) * COST_PER_MINUTE;
        return result;
    }

    public void Print()
    {
        string[] spotState = new string[TotalNumberOfParkingSpots];

        for (int i = 0; i < spotState.Length; i++)
        {
            spotState[i] = $"{i}: ";
        }

        foreach (Parking p in activeParkings)
        {

            if (p.Vehicle is Bus)
            {
                spotState[p.ParkingSpotIndex] = $"{p.ParkingSpotIndex} - {p.ParkingSpotIndex + 1}: {p.ToString()}";
                spotState[p.ParkingSpotIndex + 1] = "";
            }
            else
            {
                spotState[p.ParkingSpotIndex] += " " + p.ToString();
            }
        }


        for (int i = 0; i < TotalNumberOfParkingSpots; i++)
        {
            Console.WriteLine(spotState[i]);
        }
    }
}
