namespace TicketManagementService.Domain.ValueObjects;

public record TicketType
{
    public string Value { get; }

    private static readonly string[] AllowedTypes = { "VIP", "Standard", "Premium" };

    public TicketType(string value)
    {
        if (!AllowedTypes.Contains(value))
            throw new DomainException($"Недопустимый тип билета: {value}");

        Value = value;
    }
}