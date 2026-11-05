namespace ParkLot;

internal class ParkingSpotService
{
    public static int FindHalfSpot(ParkingZone parkingZone)
    {
        List<int> emptySpots = parkingZone.GetFreeHalfSpots();
        //score each spot based on surrounding occupation status of surrounding spots
        // sum status of neighbours and spot status
        int maxScore = 0;
        int maxScoreIndex = 0;
        //find best score candidate and return its spot index
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetHalfSpotScore(parkingZone, spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private static int GetHalfSpotScore(ParkingZone parkingZone, int spotIndex)
    {
        int result = parkingZone.GetParkingSpotStatus(spotIndex) * 10;
        result += parkingZone.GetParkingSpotStatus(spotIndex - 1);
        result += parkingZone.GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public static int FindFullSpot(ParkingZone parkingZone)
    {
        List<int> emptySpots = parkingZone.GetFreeFullSpots();
        int maxScore = 0;
        int maxScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetFullSpotScore(parkingZone, spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private static int GetFullSpotScore(ParkingZone parkingZone, int spotIndex)
    {
        int result = parkingZone.GetParkingSpotStatus(spotIndex - 1);
        result += parkingZone.GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public static int FindDoubleSpot(ParkingZone parkingZone)
    {
        List<int> emptySpots = parkingZone.GetFreeDoubleSpots();
        int maxScore = 0;
        int maxScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotScore = GetDoubleSpotScore(parkingZone, spotIndex);
            if (spotScore > maxScore)
            {
                maxScore = spotScore;
                maxScoreIndex = spotIndex;
            }
        }
        return maxScoreIndex;
    }

    private static int GetDoubleSpotScore(ParkingZone parkingZone, int spotIndex)
    {
        int result = parkingZone.GetParkingSpotStatus(spotIndex - 1);
        result += parkingZone.GetParkingSpotStatus(spotIndex + 2);
        return result;
    }
}
