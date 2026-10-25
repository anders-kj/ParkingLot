namespace ParkLot;

internal class ParkingSlotService
{
    public const int EMPTY = 0;
    public const int HALF_OCCUPIED = 1;
    public const int OCCUPIED = 2;

    public int NofParkingSlots { get { return parkingSlotStatus.Length; } }
    int[] parkingSlotStatus;

    public ParkingSlotService(int nofSlots)
    {
        parkingSlotStatus = new int[nofSlots];
    }

    private int GetParkingSlotStatus(int parkingSlotIndex)
    {
        if (parkingSlotIndex < 0 || parkingSlotIndex >= NofParkingSlots) return OCCUPIED;

        return parkingSlotStatus[parkingSlotIndex];
    }

    private List<int> GetFreeHalfSlots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSlotStatus.Length; i++)
        {
            if (parkingSlotStatus[i] == EMPTY || parkingSlotStatus[i] == HALF_OCCUPIED)
            {
                result.Add(i);
            }
        }
        return result;
    }

    private List<int> GetFreeSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSlotStatus.Length; i++)
        {
            if (parkingSlotStatus[i] == EMPTY)
            {
                result.Add(i);
            }
        }
        return result;
    }

    private List<int> GetFreeDoubleSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSlotStatus.Length - 1; i++)
        {
            if (parkingSlotStatus[i] == EMPTY && parkingSlotStatus[i + 1] == EMPTY)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public void ParkHalfSlot()
    {
        int spotIndex = FindHalfSlot();
        ParkHalfSlot(spotIndex);
    }

    private void ParkHalfSlot(int spot)
    {
        switch (parkingSlotStatus[spot])
        {
            case EMPTY:
                parkingSlotStatus[spot] = HALF_OCCUPIED;
                break;
            case HALF_OCCUPIED:
                parkingSlotStatus[spot] = OCCUPIED;
                break;
        }
    }

    private void UnParkHalfSlot(int spot)
    {
        switch (parkingSlotStatus[spot])
        {
            case OCCUPIED:
                parkingSlotStatus[spot] = HALF_OCCUPIED;
                break;
            case HALF_OCCUPIED:
                parkingSlotStatus[spot] = EMPTY;
                break;
        }
    }

    public void ParkFullSlot()
    {
        int spotIndex = FindCarSpot();
        ParkFullSpot(spotIndex);
    }

    public void ParkFullSpot(int spot)
    {
        parkingSlotStatus[spot] = OCCUPIED;
    }

    public void UnParkFullSpot(int spot)
    {
        parkingSlotStatus[spot] = EMPTY;
    }

    public void ParkDoubleSpot()
    {
        int spotIndex = FindDoubleSpot();
        ParkDoubleSpot(spotIndex);
    }

    private void ParkDoubleSpot(int spot)
    {
        parkingSlotStatus[spot] = OCCUPIED;
        parkingSlotStatus[spot + 1] = OCCUPIED;
    }

    private void UnParkDoubleSpot(int spot)
    {
        parkingSlotStatus[spot] = EMPTY;
        parkingSlotStatus[spot + 1] = EMPTY;
    }

    public void PrintSpotsOccupation()
    {
        Console.WriteLine("---- Parking Spot Status ----");
        for (int i = 0; i < parkingSlotStatus.Length; i++)
        {

            switch (parkingSlotStatus[i])
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

    public int FindHalfSlot()
    {
        List<int> emptySpots = GetFreeHalfSlots();
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

    private int GetHalfSpotScore(int slotIndex)
    {
        int result = GetParkingSlotStatus(slotIndex) * 10;
        result += GetParkingSlotStatus(slotIndex - 1);
        result += GetParkingSlotStatus(slotIndex + 1);
        return result;
    }

    public int FindCarSpot()
    {
        List<int> emptySpots = GetFreeSpots();
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
        int result = GetParkingSlotStatus(spotIndex - 1);
        result += GetParkingSlotStatus(spotIndex + 1);
        return result;
    }

    private int FindDoubleSpot()
    {
        List<int> emptySpots = GetFreeDoubleSpots();
        int maxScore = 0;
        int maxScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetDoubleSlotScore(spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private int GetDoubleSlotScore(int slotIndex)
    {
        int result = GetParkingSlotStatus(slotIndex - 1);
        result += GetParkingSlotStatus(slotIndex + 2);
        return result;
    }
}

