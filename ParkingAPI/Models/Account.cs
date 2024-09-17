public class Account
{
    public int Id { get; set; }          
    public string FamilyName { get; set; } 
    public string Email { get; set; } 
    public string Phone {get; set; }
    public ICollection<Resident> Residents { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }
    
    public Account()
    {
        FamilyName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Residents = [];
        Vehicles = [];
    }
}