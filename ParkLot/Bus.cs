namespace ParkLot;

internal class Bus : Vehicle
{
    public int NofPassengers { get; set; }

    public Bus(int nofPassengers, string regNumber, string color): base(regNumber, color) 
    {
        NofPassengers = nofPassengers;
    }

    public override string ToString()
    {
        return $"Bus, {this.RegNumber}, {NofPassengers}-passengers, {Color}";
    }
}

