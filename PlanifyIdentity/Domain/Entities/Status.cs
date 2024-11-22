using PlanifyIdentity.Database.Common;

namespace PlanifyIdentity.Domain.Entities;
internal sealed class Status : BaseAuditableEntity
{
    public string? Entity { get; set; }
    public string? Name { get; set; }
    public int? Order { get; set; }
    public bool IsEnabled { get; set; }
    public string? Description { get; set; }
}
