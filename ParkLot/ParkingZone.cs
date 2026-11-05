namespace ParkLot;

internal class ParkingZone
{

    public int NofParkingSpots { get { return parkingSpots.Length; } }
    public int NofAvailableHalfSpots { get { return GetAvailableHalfSpots().Count; } }
    public int NofAvailableFullSpots { get { return GetAvailableFullSpots().Count; } }
    public int NofAvailableDoubleSpots { get { return GetAvailableDoubleSpots().Count; } }

    private ParkingSpot[] parkingSpots;
    public ParkingZone(int nofSpots) 
    {
        parkingSpots = new ParkingSpot[nofSpots];
        InitParkingSpots();
    }

    private void InitParkingSpots()
    {
       
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            parkingSpots[i] = new ParkingSpot();
        }
    }

    public int GetParkingSpotStatus(int parkingSpotIndex)
    {
        if (parkingSpotIndex < 0 || parkingSpotIndex >= parkingSpots.Length) return ParkingSpot.OCCUPIED;

        return parkingSpots[parkingSpotIndex].Status;
    }

    public bool HasAvailableFullParking(int parkingSpotIndex)
    {
        return parkingSpots[parkingSpotIndex].IsEmpty;
    }

    public bool HasAvailableHalfParking(int parkingSpotIndex)
    {
        return (!parkingSpots[parkingSpotIndex].IsOccupied);
    }

    public bool HasAvailableDoubleParking(int parkingSpotIndex)
    {
        return parkingSpots[parkingSpotIndex].IsEmpty && parkingSpots[parkingSpotIndex + 1].IsEmpty;
    }

    public List<int> GetAvailableFullSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i].IsEmpty)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public List<int> GetAvailableHalfSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i].IsEmpty || parkingSpots[i].IsHalfOccupied)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public List<int> GetAvailableDoubleSpots()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < parkingSpots.Length - 1; i++)
        {
            if (parkingSpots[i].IsEmpty && parkingSpots[i + 1].IsEmpty)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public void ParkHalfSpot(int spot)
    {
        switch (parkingSpots[spot].Status)
        {
            case ParkingSpot.EMPTY:
                parkingSpots[spot].Status = ParkingSpot.HALF_OCCUPIED;
                break;
            case ParkingSpot.HALF_OCCUPIED:
                parkingSpots[spot].Status = ParkingSpot.OCCUPIED;
                break;
        }
    }

    public void ReleaseHalfSpot(int spot)
    {
        switch (parkingSpots[spot].Status)
        {
            case ParkingSpot.OCCUPIED:
                parkingSpots[spot].Status = ParkingSpot.HALF_OCCUPIED;
                break;
            case ParkingSpot.HALF_OCCUPIED:
                parkingSpots[spot].Status = ParkingSpot.EMPTY;
                break;
        }
    }

    public void ParkFullSpot(int spot)
    {
        parkingSpots[spot].Status = ParkingSpot.OCCUPIED;
    }

    public void ReleaseFullSpot(int spot)
    {
        parkingSpots[spot].Status = ParkingSpot.EMPTY;
    }

    public void ParkDoubleSpot(int spot)
    {
        parkingSpots[spot].Status = ParkingSpot.OCCUPIED;
        parkingSpots[spot + 1].Status = ParkingSpot.OCCUPIED;
    }

    public void ReleaseDoubleSpot(int spot)
    {
        parkingSpots[spot].Status = ParkingSpot.EMPTY;
        parkingSpots[spot + 1].Status = ParkingSpot.EMPTY;
    }
}
