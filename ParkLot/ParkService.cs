namespace ParkLot;

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

    public List<int> GetFreeHalfSpots()
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

    public List<int> GetFreeSpots()
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

    public List<int> GetFreeDoubleSpots()
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
        ParkHalfSpot(spotIndex);
    }

    public void ParkHalfSpot(int spot)
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

    public void UnParkHalfSpot(int spot)
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
        ParkFullSpot(spotIndex);
    }

    public void ParkFullSpot(int spot)
    {
        parkingSpotStatus[spot] = OCCUPIED;
    }

    public void UnParkFullSpot(int spot)
    {
        parkingSpotStatus[spot] = EMPTY;
    }

    public void ParkBus()
    {
        int spotIndex = parkingSpotFinderService.FindBusSpot();
        ParkDoubleSpot(spotIndex);
    }

    public void ParkDoubleSpot(int spot)
    {
        parkingSpotStatus[spot] = OCCUPIED;
        parkingSpotStatus[spot + 1] = OCCUPIED;
    }

    public void UnParkDoubleSpot(int spot)
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

