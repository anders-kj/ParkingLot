namespace ParkLot;

internal class Car : Vehicle
{
    public bool IsElectric { get;}
    public Car(bool isElectric, string regNumber, string color):base(regNumber, color)
    {
        IsElectric = isElectric;
    }

    public override string ToString()
    {
        string type = IsElectric ? "EV" : "Bensin";
        return $"Bil, {this.RegNumber}, {type}, {Color}";
    }
}

