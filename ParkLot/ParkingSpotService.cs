namespace ParkLot;

internal class ParkingSpotService
{
    private const int EMPTY = 0;
    private const int HALF_OCCUPIED = 1;
    private const int OCCUPIED = 2;

    private int NofParkingSpots { get { return parkingSpotStatus.Length; } }

    public int NofFreeHalfSpots { get { return GetFreeHalfSpots().Count; } }

    public int NofFreeFullSpots { get { return GetFreeFullSpots().Count; } }
    public int NofFreeDoubleSpots { get { return GetFreeDoubleSpots().Count; } }
    int[] parkingSpotStatus;

    //ParkingSpot[] parkingSlots;

    public ParkingSpotService(int nofSpots)
    {
        parkingSpotStatus = new int[nofSpots];
        //parkingSlots = new ParkingSpot[nofSpots];
    }

    private int GetParkingSpotStatus(int parkingSpotIndex)
    {
        if (parkingSpotIndex < 0 || parkingSpotIndex >= NofParkingSpots) return OCCUPIED;

        return parkingSpotStatus[parkingSpotIndex];
    }

    private List<int> GetFreeHalfSpots()
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

    private List<int> GetFreeFullSpots()
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

    private List<int> GetFreeDoubleSpots()
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

    public int ParkHalfSpot()
    {
        int spotIndex = FindHalfSpot();
        ParkHalfSpot(spotIndex);
        return spotIndex;
    }

    private void ParkHalfSpot(int spot)
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

    public void ReleaseHalfSpot(int spot)
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

    public int ParkFullSpot()
    {
        int spotIndex = FindFullSpot();
        ParkFullSpot(spotIndex);
        return spotIndex;
    }

    public void ParkFullSpot(int spot)
    {
        parkingSpotStatus[spot] = OCCUPIED;
    }

    public void ReleaseFullSpot(int spot)
    {
        parkingSpotStatus[spot] = EMPTY;
    }

    public int ParkDoubleSpot()
    {
        int spotIndex = FindDoubleSpot();
        ParkDoubleSpot(spotIndex);
        return spotIndex;
    }

    private void ParkDoubleSpot(int spot)
    {
        parkingSpotStatus[spot] = OCCUPIED;
        parkingSpotStatus[spot + 1] = OCCUPIED;
    }

    public void ReleaseDoubleSpot(int spot)
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

    public int FindHalfSpot()
    {
        List<int> emptySpots = GetFreeHalfSpots();
        //score each spot based on surrounding occupation status of surrounding spots
        // sum status of neighbours and spot status
        int maxScore = 0;
        int maxScoreIndex = 0;
        //find best score candidate and return its spot index
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetHalfSpotScore(spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private int GetHalfSpotScore(int spotIndex)
    {
        int result = GetParkingSpotStatus(spotIndex) * 10;
        result += GetParkingSpotStatus(spotIndex - 1);
        result += GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public int FindFullSpot()
    {
        List<int> emptySpots = GetFreeFullSpots();
        int maxScore = 0;
        int maxScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetFullSpotScore(spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private int GetFullSpotScore(int spotIndex)
    {
        int result = GetParkingSpotStatus(spotIndex - 1);
        result += GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    private int FindDoubleSpot()
    {
        List<int> emptySpots = GetFreeDoubleSpots();
        int maxScore = 0;
        int maxScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetDoubleSpotScore(spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private int GetDoubleSpotScore(int spotIndex)
    {
        int result = GetParkingSpotStatus(spotIndex - 1);
        result += GetParkingSpotStatus(spotIndex + 2);
        return result;
    }
}


//internal class ParkingSpot
//{
//    private const int EMPTY = 0;
//    private const int HALF_OCCUPIED = 1;
//    private const int OCCUPIED = 2;
//    public int Status { get; set; }

//    public ParkingSpot()
//    {
//        Status = EMPTY;
//    }
//}