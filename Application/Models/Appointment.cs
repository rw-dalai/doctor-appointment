namespace Application.Models;

public class Appointment
{
    // PK
    public int Id { get; set; }

    public DateOnly Date { get; set; }
    
    public DateTime Created { get; set; }
    
    // Navigation property
    public Patient Patient { get; set; }
    
    // Shadow FK
    // public int PatientId { get; set; }
    
    // Navigation property
    public AppointmentState? CurrentState { get; set; }
    
    
    // --- EF Ctor ---
    protected Appointment() {}
    
    // --- Business Ctor ---
    public Appointment(
        DateOnly date, 
        DateTime created, 
        Patient patient, 
        AppointmentState? currentState)
    {
        
        Date = date;
        Created = created;
        Patient = patient;
        CurrentState = currentState;
        
    }
}