namespace Application.Models;

// RichType
// 1) Making illegal states unrepresentable
// 2) Immutable
// 3) Equality: structurial equality
public record InsuranceNumber
{
    public string Value { get; }
    
    
    // --- EF Ctor ---
    protected InsuranceNumber() { }

    // --- Business Ctor ---

    // "  lol  ".Trim() -> ""
    public InsuranceNumber(string value)
    {
        var trimmedValue = value.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue) || trimmedValue.Length != 10)
            throw new AppointmentException($"'{value}' is not a valid insurance number.");

        Value = trimmedValue;
    }
}