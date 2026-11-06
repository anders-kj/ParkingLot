namespace ParkLot;

internal class Parking(Vehicle vehicle, int spotNumber)
{
    private DateTimeOffset StartTime { get; } = DateTimeOffset.UtcNow;
    public int ParkingTimeSeconds { get { return (int)(DateTimeOffset.UtcNow - StartTime).TotalSeconds; } }
    public Vehicle Vehicle { get; } = vehicle;
    public int ParkingSpotIndex { get; } = spotNumber;


    public override string ToString()
    {
        return $"[{Vehicle.ToString()}, {StartTime:HH:mm}]";
    }

    public bool ContainVehicleWith(string regNumber)
    {
        return regNumber.CompareTo(Vehicle.RegNumber, StringComparison.OrdinalIgnoreCase) == 0;
    }
}
