namespace Application.Models;

public abstract class AppointmentState
{
    public int Id { get; set; }
    
    public Appointment Appointment { get; set; }

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