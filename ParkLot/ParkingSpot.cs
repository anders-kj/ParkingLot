namespace ParkLot;

internal class ParkingSpot
{
    public const int EMPTY = 0;
    public const int HALF_OCCUPIED = 1;
    public const int OCCUPIED = 2;
    public int Status { get; set; }

    public bool IsEmpty { get { return Status == EMPTY; }}
    public bool IsHalfOccupied { get { return Status == HALF_OCCUPIED; } }

    public ParkingSpot()
    {
        Status = EMPTY;
    }


}