using static ParkLot.Program;

namespace ParkLot;

internal class ParkingApp
{
    private ParkingService parkingService;

    public ParkingApp(ParkingService parkService)
    {
        this.parkingService = parkService;
    }

    public void Start()
    {
        Car nextCar = VehicleFactory.CreateNewCar();
        MotorCycle nextMC = VehicleFactory.CreateNewMC();
        Bus nextBus = VehicleFactory.CreateNewBus();

        while (true)
        {
            Console.Clear();
            parkingService.Print();
            Console.WriteLine();
            Console.WriteLine("--ParkAdmin----------------------------------------");
            Console.WriteLine($"[F1] Parkera {nextMC.ToString()}");
            Console.WriteLine($"[F2] Parkera {nextCar.ToString()}");
            Console.WriteLine($"[F3] Parkera {nextBus.ToString()}");
            Console.WriteLine("[F4] Avsluta Parkering");
            Console.WriteLine("---------------------------------------------------");
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.F1:
                    parkingService.Park(nextMC);
                    nextMC = VehicleFactory.CreateNewMC();
                    Console.ReadKey();
                    break;
                case ConsoleKey.F2:
                    parkingService.Park(nextCar);
                    nextCar = VehicleFactory.CreateNewCar();
                    Console.ReadKey();
                    break;
                case ConsoleKey.F3:
                    parkingService.Park(nextBus);
                    nextBus = VehicleFactory.CreateNewBus();
                    Console.ReadKey();
                    break;

                case ConsoleKey.F4:
                    Console.Write("UtCheckning - Ange Registreringsnummer :");
                    string? regNumber = Console.ReadLine();

                    parkingService.CheckOut(regNumber);
                    Console.ReadKey();
                    break;
            }
        }
    }
}
