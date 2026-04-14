namespace Frendy.CRM.Shared.Models;

public record ModalInstance(
    Type ComponentType,
    Dictionary<string, object> Parameters
);