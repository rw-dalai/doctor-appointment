namespace Application.Models;

public class ConfirmedAppointmentState : AppointmentState
{
    public Doctor  Doctor { get; set; }
    
    public TimeSlot PlannedSlot { get; set; }
    
    public string? Infotext { get; set; }
    
    
    // --- EF Ctor ---
    
    public ConfirmedAppointmentState() { }
    
    
    // --- Business Ctor ---
    
    public ConfirmedAppointmentState(
        Appointment Appointment,
        DateTime Created,
        Doctor doctor,
        TimeSlot plannedSlot,
        string? infotext
    ) : base(Appointment, Created)
    {
        Doctor = doctor;
        PlannedSlot = plannedSlot;
        Infotext = infotext;
    }
        
}