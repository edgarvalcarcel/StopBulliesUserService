using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanifyIdentity.Database;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");
       
        builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Role"));
        builder.Entity<IdentityUser>(entity => entity.ToTable(name: "User"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaim"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogin"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRole"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserToken"));
       
        


    }
}
