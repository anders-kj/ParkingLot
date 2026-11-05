namespace ParkLot;

internal partial class Program
{
    internal class VehicleFactory
    {
        private static int regNumber = 100;

        public static MotorCycle CreateNewMC()
        {
           
            MotorCycle result = new MotorCycle(GetRandomMCManufacturer(), CreateRandomRegNumber(), GetRandomVehicleColor());
            return result;
        }

        public static Car CreateNewCar()
        {
            Car result = new Car(Random.Shared.Next(2) == 0, CreateRandomRegNumber(), GetRandomVehicleColor());
            return result;
        }

        public static Bus CreateNewBus()
        {
            Bus result = new Bus(Random.Shared.Next(10, 50), CreateRandomRegNumber(), GetRandomVehicleColor());
            return result;
        }


        private static string CreateRandomRegNumber()
        {
            string result = "";
            result += (char)Random.Shared.Next('a', 'z' + 1);
            result += (char)Random.Shared.Next('a', 'z' + 1);
            result += (char)Random.Shared.Next('a', 'z' + 1);
            result += Random.Shared.Next(100, 1000);
            return result;
        }

        private static string GetRandomMCManufacturer()
        {
            return motorcycleManufacturers[Random.Shared.Next(0, motorcycleManufacturers.Length)];
        }

        static string[] motorcycleManufacturers = new string[10]
        {
            "Yamaha",
            "Honda",
            "Kawasaki",
            "Suzuki",
            "BMW Motorrad",
            "Ducati",
            "Harley-Davidson",
            "Triumph",
            "KTM",
            "Husqvarna"
        };

        private static string GetRandomVehicleColor()
        {
            return vehicleColors[Random.Shared.Next(0, vehicleColors.Length)];
        }

        static string[] vehicleColors = new string[10]
        {
            "Svart",
            "Röd",
            "Blå",
            "Vit",
            "Silver",
            "Grå",
            "Grön",
            "Gul",
            "Orange",
            "Koppar"
        };
    }
}

