using Planify.Identity.Database;
using System.Security.Claims;
using PlanifyIdentity.Infrastructure;

namespace PlanifyIdentity.Endpoints;

public class Identity : IEndpointGroupBase
{
    public void Map(WebApplication app)
    {
        app.MapGet("User/me", async (ClaimsPrincipal claims, ApplicationDbContext context) =>
        {
            string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return await context.Users.FindAsync(userId);
        })
        .RequireAuthorization();
    }
}
