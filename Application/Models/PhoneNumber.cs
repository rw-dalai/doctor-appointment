namespace Application.Models;

// https://github.com/google/libphonenumber
public class PhoneNumber
{
    public string Value { get; }
    
    public PhoneNumber(string value)
    {
        Value = value;
    }
}