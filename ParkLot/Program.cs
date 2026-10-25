namespace ParkLot;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ParkLot_1.0");
        ParkService ps = new ParkService(nofSpots: 10);

        ps.ParkCar(1);
        ps.ParkBike(7);
        ps.PrintSpotsOccupation();
        ps.ParkBus();
        ps.PrintSpotsOccupation();
        ps.ParkBus();
        ps.PrintSpotsOccupation();
        ps.ParkBus();
        ps.PrintSpotsOccupation();

        Console.WriteLine( "auto select--->");
        ps = new ParkService(10);
        ps.ParkBike();
        ps.ParkCar();
        ps.PrintSpotsOccupation();
        ps.ParkBus();
        ps.PrintSpotsOccupation();
        ps.ParkBus();
        ps.PrintSpotsOccupation();
        ps.ParkBus();
        ps.PrintSpotsOccupation();


    }
} 

internal class ParkService
{

    //Dictionary<int, ParkingSpot> spots = new Dictionary<int, ParkingSpot>();

    ParkingSpotFinderService parkingSpotFinderService;

    public const int EMPTY = 0;
    public const int HALF_OCCUPIED = 1;
    public const int OCCUPIED = 2;

    public int NofParkingSpots { get { return parkingSpotStatus.Length; } }
    //public int ParkingSpotStatus { get { } }
    int[] parkingSpotStatus;

    public ParkService(int nofSpots)
    {
        parkingSpotStatus = new int[nofSpots];
        parkingSpotFinderService = new ParkingSpotFinderService(this);
    }

    public int GetParkingSpotStatus(int parkingSpotIndex)
    {
        if (parkingSpotIndex < 0 || parkingSpotIndex >= NofParkingSpots) return OCCUPIED;

        return parkingSpotStatus[parkingSpotIndex];
    }

    public List<int> GetFreeBikeSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSpotStatus.Length; i++)
        {
            if (parkingSpotStatus[i] == EMPTY || parkingSpotStatus[i] == HALF_OCCUPIED)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public List<int> GetFreeCarSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSpotStatus.Length; i++)
        {
            if (parkingSpotStatus[i] == EMPTY)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public List<int> GetFreeBusSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSpotStatus.Length - 1; i++)
        {
            if (parkingSpotStatus[i] == EMPTY && parkingSpotStatus[i + 1] == EMPTY)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public void ParkBike()
    {
        int spotIndex = parkingSpotFinderService.FindBikeSpot();
        ParkBike(spotIndex);
    }

    public void ParkBike(int spot)
    {
        switch (parkingSpotStatus[spot])
        {
            case EMPTY:
                parkingSpotStatus[spot] = HALF_OCCUPIED;
                break;
            case HALF_OCCUPIED:
                parkingSpotStatus[spot] = OCCUPIED;
                break;
        }
    }

    public void UnParkBike(int spot)
    {
        switch (parkingSpotStatus[spot])
        {
            case OCCUPIED:
                parkingSpotStatus[spot] = HALF_OCCUPIED;
                break;
            case HALF_OCCUPIED:
                parkingSpotStatus[spot] = EMPTY;
                break;
        }
    }

    public void ParkCar()
    {
        int spotIndex = parkingSpotFinderService.FindCarSpot();
        ParkCar(spotIndex);
    }

    public void ParkCar(int spot)
    {
        parkingSpotStatus[spot] = OCCUPIED;
    }

    public void UnParkCar(int spot)
    {
        parkingSpotStatus[spot] = EMPTY;
    }

    public void ParkBus()
    {
        int spotIndex = parkingSpotFinderService.FindBusSpot();
        ParkBus(spotIndex);
    }

    public void ParkBus(int spot)
    {
        parkingSpotStatus[spot] = OCCUPIED;
        parkingSpotStatus[spot + 1] = OCCUPIED;
    }

    public void UnParkBus(int spot)
    {
        parkingSpotStatus[spot] = EMPTY;
        parkingSpotStatus[spot + 1] = EMPTY;
    }

    public void PrintSpotsOccupation()
    {
        Console.WriteLine("---- Parking Spot Status ----");
        for (int i = 0; i < parkingSpotStatus.Length; i++)
        {

            switch (parkingSpotStatus[i])
            {
                case EMPTY:
                    Console.WriteLine($"{i} : Tom");
                    break;
                case HALF_OCCUPIED:
                    Console.WriteLine($"{i} : Halvt-Upptagen");
                    break;
                case OCCUPIED:
                    Console.WriteLine($"{i} : Upptagen");
                    break;
            }
        }
    }
}

internal class ParkingSpotFinderService
{
    ParkService parkingSpotService;
    public ParkingSpotFinderService(ParkService service)
    {
        this.parkingSpotService = service;
    }

    public int FindBikeSpot()
    {
        List<int> emptySpots = parkingSpotService.GetFreeBikeSpots();
        //score each spot based on surrounding occupation status of surrounding spots
        // sum status of neighbours and spot status
        int maxScore = 0;
        int maxScoreIndex = 0;
        //find best score candidate and return its spot index
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetSpotScoreBike(spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private int GetSpotScoreBike(int spotIndex)
    {
        int result = parkingSpotService.GetParkingSpotStatus(spotIndex);
        result += parkingSpotService.GetParkingSpotStatus(spotIndex - 1);
        result += parkingSpotService.GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public int FindCarSpot()
    {
        List<int> emptySpots = parkingSpotService.GetFreeCarSpots();
        int maxScore = 0;
        int maxScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetSpotScoreCar(spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private int GetSpotScoreCar(int spotIndex)
    {
        int result = parkingSpotService.GetParkingSpotStatus(spotIndex - 1);
        result += parkingSpotService.GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public int FindBusSpot()
    {
        List<int> emptySpots = parkingSpotService.GetFreeBusSpots();
        int maxScore = 0;
        int maxScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetSpotScoreBus(spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private int GetSpotScoreBus(int spotIndex)
    {
        int result = parkingSpotService.GetParkingSpotStatus(spotIndex - 1);
        result += parkingSpotService.GetParkingSpotStatus(spotIndex + 2);
        return result;
    }
}

