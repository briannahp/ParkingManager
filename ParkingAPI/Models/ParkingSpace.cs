public class ParkingSpace
{
    public int Id { get; set; }      
    public bool IsTaken { get; set; }
    public int? VehicleId { get; set; }        
    public Vehicle? Vehicle { get; set; }
     public ParkingSpace()
    {
    }
}