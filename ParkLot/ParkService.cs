namespace ParkLot;

internal class ParkService
{
    private const decimal COST_PER_MINUTE = 1.5M;
    //ParkingSpotService parkingSpotService;
    private ParkingZone parkingZone;
    private List<Parking> activeParkings;
    public int TotalNumberOfParkingSpots { get; }


    public ParkService(int nofSpots)
    {
        //parkingSpotService = new ParkingSpotService(nofSpots);
        parkingZone = new ParkingZone(nofSpots);
        activeParkings = new List<Parking>();
        TotalNumberOfParkingSpots = nofSpots;
    }

    public void Park(MotorCycle bike)
    {
        if (parkingZone.NofFreeHalfSpots > 0)
        {
            int spotNumber = ParkingSpotService.FindHalfSpot(parkingZone);
            AddNewParking(bike, spotNumber);
        }
        else
        {
            Console.WriteLine($"No room for Bike {bike.ToString()}. Welcome Back Later!");
        }
    }

    public void Park(Car car)
    {
        if (parkingZone.NofFreeFullSpots > 0)
        {
            int spotNumber = ParkingSpotService.FindFullSpot(parkingZone);

            AddNewParking(car, spotNumber);
        } else
        {
            Console.WriteLine($"No room for {car.ToString()}. Welcome Back Later!");
        }
    }

    public void Park(Bus bus)
    {
        if (parkingZone.NofFreeDoubleSpots > 0)
        {
            int spotNumber = ParkingSpotService.FindDoubleSpot(parkingZone);
            AddNewParking(bus, spotNumber);
        } else
        {
            Console.WriteLine($"No room for {bus.ToString()}. Welcom Back Later!");
        }
    }

    private void AddNewParking(Vehicle v, int spotIndex)
    {
        Parking p = new Parking(v, spotIndex);
        activeParkings.Add(p);

        switch(v)
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
        Console.WriteLine($"Parking {v.ToString()} on spot {spotIndex}");
    }

    

    private Parking? GetActiveParkingFor(string regNumber)
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
        Console.WriteLine($"Checkar ut fordon med RegistreringsNummer {regNumber}");
        Parking p = GetActiveParkingFor(regNumber);
        if (p != null)
        {
            Console.WriteLine($"Avslutar parkering för {p.ToString()} Total Kostnad : {CalculateCost(p):F2} kr");
            CheckOut(p);
            result = true;
        }
        return result;
    }

    private void CheckOut(Parking p)
    {
        activeParkings.Remove(p);

        switch (p.Vehicle)
        {
            case MotorCycle m:
                parkingZone.ReleaseHalfSpot(p.ParkingSpotIndex);
                break;
            case Car c:
                parkingZone.ReleaseFullSpot(p.ParkingSpotIndex);
                break;
            case Bus b:
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


internal class Parking(Vehicle vehicle, int spotNumber)
{
    private DateTimeOffset StartTime { get; } = DateTimeOffset.UtcNow;
    public int ParkingTimeSeconds { get { return (int)(DateTimeOffset.UtcNow - StartTime).TotalSeconds; } }
    public Vehicle Vehicle { get; } = vehicle;
    public int ParkingSpotIndex { get; } = spotNumber;

    public void Print()
    {
        Console.WriteLine($"{Vehicle} Time:{ParkingTimeSeconds}s");
    }

    public override string ToString()
    {
        return $"[{Vehicle.ToString()}, {StartTime:HH:mm}]";
    }

    public bool ContainVehicleWith(string regNumber)
    {
        return regNumber.CompareTo(Vehicle.RegNumber, StringComparison.OrdinalIgnoreCase) == 0;
    }
}
