namespace Application.Models;

// Entity
public class Patient
{
    // PK
    public int Id { get; set; }
    
    public string Firstname { get; set; }
    
    public string Lastname { get; set; }
    
    public InsuranceNumber InsuranceNumber { get; set; }
    
    public PhoneNumber? Mobile { get; set; }
    
    
    // --- EF Ctor ---
    protected Patient() { }
    
    // --- Business Ctor ---
    public Patient(
        string firstname,
        string lastname,
        InsuranceNumber insuranceNumber,
        PhoneNumber? mobile)
    {
        Firstname = firstname;
        Lastname = lastname;
        InsuranceNumber = insuranceNumber;
        Mobile = mobile;
    }
        
    
}