namespace Frendy.CRM.Shared.Models;

public class AuditDetails : AuditShortDetailsLookup
{
    public string? ExecutorEmail { get; set; }
    public required string ExecutorDeviceName { get; set; }
    public required string ExecutorCity { get; set; }
    public string? ActionDescription { get; set; }
    public string? ExecutorPhoneNumber { get; set; }
    public required string ExecutorIpAddress { get; set; }
    public string? ExecutorUsername { get; set; }
    public string? TargetEmail { get; set; }
    public string? TargetPhoneNumber { get; set; }
    public string? TargetUsername { get; set; }
}