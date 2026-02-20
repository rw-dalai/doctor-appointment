namespace Application.Models;

public class CancelledAppointmentState : AppointmentState
{
    // --- EF Ctor ---
    
    protected CancelledAppointmentState()
    { }
    
    
    // --- Business Ctor ---
    
    public CancelledAppointmentState(
        Appointment appointment,
        DateTime created
    ) : base(appointment, created)
    {
    }
}