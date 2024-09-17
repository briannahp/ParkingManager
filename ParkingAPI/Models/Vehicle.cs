public class Vehicle
{
    public int Id { get; set; }          
    public string LicensePlate { get; set; }    
    public string Make { get; set; }            
    public string Model { get; set; }
    public string Color { get; set; }
    public int AccountId { get; set; }
    public int? SpotId { get; set; }
    public ParkingSpace? ParkingSpace { get; set; }
    public Account? Account {get; set;}

     public Vehicle()
    {
        LicensePlate = string.Empty;
        Make = string.Empty;
        Model = string.Empty;
        Color = string.Empty;
    }
}