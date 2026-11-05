using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace ParkLot;

internal partial class Program
{
    static void Main(string[] args)
    {
        ParkingService ps = new ParkingService(nofSpots: 15);

        ps.Park(VehicleFactory.CreateNewCar(), 0);
        ps.Park(VehicleFactory.CreateNewCar(), 3);
        ps.Park(VehicleFactory.CreateNewMC(), 5);
        ps.Park(VehicleFactory.CreateNewMC(), 7);
        ps.Park(VehicleFactory.CreateNewMC(), 7);
        ps.Park(VehicleFactory.CreateNewCar(), 13);
        ps.Park(VehicleFactory.CreateNewBus(), 10);


        ParkingAdmin admin = new ParkingAdmin(ps);
        //Console.WriteLine("Press Key To start ParkAdmin.");
        //Console.ReadKey();
        admin.Start();
    }
}
