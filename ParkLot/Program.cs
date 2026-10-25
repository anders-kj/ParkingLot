namespace ParkLot;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ParkLot_1.0");
        ParkService ps = new ParkService(nofSlots: 10);

        Car c1 = new Car(false, "123", "grå");
        Car c2 = new Car(false, "123", "grå");
        Car c3 = new Car(false, "123", "grå");

        MotorCycle b1 = new MotorCycle("honda", "234", "grå");

        ps.Park(c1);
        ps.Park(b1);
        ps.Park(c2);
        ps.Park(c3);

        ps.Print();


    }
} 

