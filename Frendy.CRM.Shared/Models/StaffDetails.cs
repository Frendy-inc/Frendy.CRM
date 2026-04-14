namespace Frendy.CRM.Shared.Models;

public class StaffDetails : StaffShortDetailsLookup
{
    public string? Avatar { get; set; }
    public bool IsBanned { get; set; }
    public string? Username { get; set; }
    public string DeviceName { get; set; } = null!;
    public string? AssignerEmail { get; set; }
    public string? AssignerPhoneNumber { get; set; }
    public string? AssignerUsername { get; set; }
}