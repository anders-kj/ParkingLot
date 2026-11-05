namespace ParkLot;

internal class Vehicle
{
    public string RegNumber { get;}
    public string Color { get;}
    public Vehicle(string regNumber, string color)
    {
        RegNumber = regNumber;
        Color = color;
    }

    public override string ToString()
    {
        return $"RegNr {RegNumber} Color {Color}";
    }
}

