namespace ParkLot;

internal class ParkingSpotFinderService
{
    ParkService parkingSpotService;
    public ParkingSpotFinderService(ParkService service)
    {
        this.parkingSpotService = service;
    }

    public int FindBikeSpot()
    {
        List<int> emptySpots = parkingSpotService.GetFreeHalfSpots();
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
        int result = parkingSpotService.GetParkingSpotStatus(spotIndex) * 10;
        result += parkingSpotService.GetParkingSpotStatus(spotIndex - 1);
        result += parkingSpotService.GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public int FindCarSpot()
    {
        List<int> emptySpots = parkingSpotService.GetFreeSpots();
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
        List<int> emptySpots = parkingSpotService.GetFreeDoubleSpots();
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

