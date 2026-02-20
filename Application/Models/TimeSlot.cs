namespace Application.Models;

public record TimeSlot(TimeOnly start, TimeOnly end)
{
    public TimeOnly Start { get; } = start;
    public TimeOnly End { get; } = end;
}