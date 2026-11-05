using static ParkLot.Program;

namespace ParkLot;

internal class ParkingAdmin
{
    private ParkingService parkingService;

    public ParkingAdmin(ParkingService parkService)
    {
        this.parkingService = parkService;
    }

    public void Start()
    {
        bool doRun = true;

        Car nextCar = VehicleFactory.CreateNewCar();
        MotorCycle nextMC = VehicleFactory.CreateNewMC();
        Bus nextBus = VehicleFactory.CreateNewBus();

        while (doRun)
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
                    Console.WriteLine($"Försöker Parkera MotorCykeln: {nextMC}");
                    parkingService.Park(nextMC);
                    nextMC = VehicleFactory.CreateNewMC();
                    Console.ReadKey();
                    break;
                case ConsoleKey.F2:
                    Console.WriteLine($"Försöker Parkera Bilen: {nextCar}");
                    parkingService.Park(nextCar);
                    nextCar = VehicleFactory.CreateNewCar();
                    Console.ReadKey();
                    break;
                case ConsoleKey.F3:
                    Console.WriteLine($"Försöker Parkera Bussen: {nextBus}");
                    parkingService.Park(nextBus);
                    nextBus = VehicleFactory.CreateNewBus();
                    Console.ReadKey();
                    break;

                case ConsoleKey.F4:
                    Console.Write("UtCheckning - Ange Registreringsnummer :");
                    string? regNumber = Console.ReadLine();

                    if (parkingService.CheckOut(regNumber))
                    {
                        Console.WriteLine("Fordon lämnar parkering.");
                    }
                    else
                    {
                        Console.WriteLine("Hittar ej Registreringsnummer för Aktiva Parkerade fordon.");
                    }
                    Console.ReadKey();
                    break;
            }
        }
    }
}
