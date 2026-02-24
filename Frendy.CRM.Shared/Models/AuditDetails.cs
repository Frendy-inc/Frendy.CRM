namespace Frendy.CRM.Shared.Models;

public class AuditDetails : AuditShortDetailsLookup
{
    public string? Email { get; set; }
    public required string DeviceName { get; set; }
    public required string City { get; set; }
    public string? ActionDescription { get; set; }
}