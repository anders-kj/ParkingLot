namespace ParkLot;

internal class MotorCycle: Vehicle
{
    public string Manufacturer {  get;}
    public MotorCycle(string manufacturer, string regNumber, string color):base(regNumber, color)
    {
        Manufacturer = manufacturer;
    }
}

