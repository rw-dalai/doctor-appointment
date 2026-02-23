namespace Application.Models;

// anmic domain model (no behavior, just data)
// rich domain model (with behavior) 
public class Doctor
{
    // PK
    public int Id { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }


    // --- EF Ctor ---

    protected Doctor() { }

    // --- Business Ctor ---

    public Doctor(string firstname, string lastname, string email)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
    }
}