namespace ParkLot;

internal class Car : Vehicle
{
    public bool IsElectric { get;}
    public Car(bool isElectric, string regNumber, string color):base(regNumber, color)
    {
        IsElectric = isElectric;
    }
}

