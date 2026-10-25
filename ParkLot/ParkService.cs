namespace ParkLot;

internal class ParkService
{
    ParkingSlotService parkingSlotService;

    
    public ParkService(int nofSlots)
    {
        parkingSlotService = new ParkingSlotService(nofSlots);
    }

    public void Park(MotorCycle bike)
    {
        parkingSlotService.ParkHalfSlot();
    }
    public void Park(Car car)
    {
        parkingSlotService.ParkFullSlot();
    }

    public void Park(Bus bus)
    {
        parkingSlotService.ParkDoubleSpot();
    }

    public void Print()
    {
        parkingSlotService.PrintSpotsOccupation();
    }
   
}

