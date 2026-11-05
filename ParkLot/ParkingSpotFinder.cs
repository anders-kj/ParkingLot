namespace ParkLot;

internal class ParkingSpotFinder
{
    public static int FindHalfSpot(ParkingZone parkingZone)
    {
        //Lista lediga platser som har utrymme för ett halvt fordon
        List<int> emptySpots = parkingZone.GetFreeHalfSpots();

        //Hitta den lediga plats i listan som packar ihop fordon på bästa sätt         
        int maxFillScore = 0;
        int maxFillScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            //För varje ledig plats i listan beräknas en poäng som baseras på hur tätt fordonen parkeras omgivande fordon.
            //Ju Mindre fria ytor runt platsen desto högre poäng.
            int spotFillScore = GetHalfSpotFillScore(parkingZone, spotIndex);
            if (spotFillScore > maxFillScore)
            {
                maxFillScore = spotFillScore;
                maxFillScoreIndex = spotIndex;
            }
        }
        return maxFillScoreIndex;
    }

    private static int GetHalfSpotFillScore(ParkingZone parkingZone, int spotIndex)
    {
        int result = parkingZone.GetParkingSpotStatus(spotIndex) * 10;
        result += parkingZone.GetParkingSpotStatus(spotIndex - 1);
        result += parkingZone.GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public static int FindFullSpot(ParkingZone parkingZone)
    {
        List<int> emptySpots = parkingZone.GetFreeFullSpots();
        int maxFillScore = 0;
        int maxFillScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotFillScore = GetFullSpotFillScore(parkingZone, spotIndex);
            if (spotFillScore > maxFillScore)
            {
                maxFillScore = spotFillScore;
                maxFillScoreIndex = spotIndex;
            }
        }
        return maxFillScoreIndex;
    }

    private static int GetFullSpotFillScore(ParkingZone parkingZone, int spotIndex)
    {
        int result = parkingZone.GetParkingSpotStatus(spotIndex - 1);
        result += parkingZone.GetParkingSpotStatus(spotIndex + 1);
        return result;
    }

    public static int FindDoubleSpot(ParkingZone parkingZone)
    {
        List<int> emptySpots = parkingZone.GetFreeDoubleSpots();
        int maxFillScore = 0;
        int maxFillScoreIndex = 0;
        foreach (int spotIndex in emptySpots)
        {
            int spotFillScore = GetDoubleSpotFillScore(parkingZone, spotIndex);
            if (spotFillScore > maxFillScore)
            {
                maxFillScore = spotFillScore;
                maxFillScoreIndex = spotIndex;
            }
        }
        return maxFillScoreIndex;
    }

    private static int GetDoubleSpotFillScore(ParkingZone parkingZone, int spotIndex)
    {
        int result = parkingZone.GetParkingSpotStatus(spotIndex - 1);
        result += parkingZone.GetParkingSpotStatus(spotIndex + 2);
        return result;
    }
}
