public class Resident {
    public int Id { get; set; }     
    public string FirstName { get; set; }   
    public string LastName { get; set; }   
    public string? AltPhoneNumber { get; set; }
    public int AccountId { get; set; }
    public Account? Account { get; set; }
    public Resident()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

}