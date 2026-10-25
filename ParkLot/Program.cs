namespace ParkLot;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ParkLot_1.0");
        ParkService ps = new ParkService(nofSpots: 10);

        ps.ParkFullSpot(1);
        ps.ParkHalfSpot(7);
        ps.PrintSpotsOccupation();
        ps.ParkBike();
        ps.PrintSpotsOccupation();
        ps.ParkBike();
        ps.PrintSpotsOccupation();
        ps.ParkBike();
        ps.PrintSpotsOccupation();
        /*
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
        */

    }
} 

