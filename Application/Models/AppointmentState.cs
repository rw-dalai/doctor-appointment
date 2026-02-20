namespace Application.Models;

public abstract class AppointmentState
{
    public int Id { get; set; }
    
    // Navigation Property
    public Appointment Appointment { get; set; }
    
    // Shadow FK
    // public int AppointmentId { get; set; }

    public DateTime Created { get; set; }

    public string Type { get; set; }
    
    // --- EF Ctor ---
    
    protected AppointmentState() { }
    
    // --- Business Ctor ---
    
    public AppointmentState(Appointment appointment, DateTime created)
    {
        Appointment = appointment;
        Created = created;
    }
}