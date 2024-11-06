

namespace PlanifyIdentity.Extensions;

public class IdentitySecrets
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? OutgoingServer { get; set; }
    public int? SMTPPort { get; set; }
}
