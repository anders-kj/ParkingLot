using System.Runtime.InteropServices;

namespace ParkLot;

internal class Program
{
    static void Main(string[] args)
    {
        ParkService ps = new ParkService(nofSpots: 15);
        bool doRun = true;

        Car nextCar = VehicleFactory.CreateNewCar();
        MotorCycle nextMC = VehicleFactory.CreateNewMC();
        Bus nextBus = VehicleFactory.CreateNewBus();

        while (doRun)
        {
            Console.Clear();
            ps.Print();

            Console.WriteLine($"[F1] Ny MC Parkering {nextMC.ToString()}");
            Console.WriteLine($"[F2] Ny Bil Parkering {nextCar.ToString()}");
            Console.WriteLine($"[F3] Ny Bus Parkering {nextBus.ToString()}");
            Console.WriteLine("[F4] Avsluta Parkering");
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.F1:
                    Console.WriteLine($"Försöker Parkera MotorCykeln: {nextMC}");
                    ps.Park(nextMC);
                    nextMC = VehicleFactory.CreateNewMC();
                    Console.ReadKey();
                    break;
                case ConsoleKey.F2:
                    Console.WriteLine($"Försöker Parkera Bilen: {nextCar}");
                    ps.Park(nextCar);
                    nextCar = VehicleFactory.CreateNewCar();
                    Console.ReadKey();
                    break;
                case ConsoleKey.F3:
                    Console.WriteLine($"Försöker Parkera Bussen: {nextBus}");
                    ps.Park(nextBus);
                    nextBus= VehicleFactory.CreateNewBus();
                    Console.ReadKey();
                    break;

                case ConsoleKey.F4:
                    Console.Write("UtCheckning - Ange Registreringsnummer :");
                    string? regNumber = Console.ReadLine();

                    if (ps.CheckOut(regNumber))
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

    internal class VehicleFactory()
    {
        private static int regNumber = 100;

        public static MotorCycle CreateNewMC()
        {
            string manufacturer = Random.Shared.Next(0, 3) > 1 ? "HarleyDavidson" : "Honda";
            MotorCycle result = new MotorCycle(manufacturer, (regNumber += 10).ToString(), "röd_" + Random.Shared.Next(0, 10));
            return result;
        }

        public static Car CreateNewCar()
        {
            Car result = new Car(Random.Shared.Next(2) == 0, (regNumber += 10).ToString(), "blå_" + Random.Shared.Next(0, 10));
            return result;
        }

        public static Bus CreateNewBus()
        {
            Bus result = new Bus(Random.Shared.Next(10, 50), (regNumber += 10).ToString(), "grön_" + Random.Shared.Next(0, 10));
            return result;
        }
    }
}

