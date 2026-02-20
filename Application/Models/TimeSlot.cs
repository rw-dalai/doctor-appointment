namespace Application.Models;

public class TimeSlot
{
    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    
    public TimeSlot(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }
}