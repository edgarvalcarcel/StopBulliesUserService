using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanifyIdentity.Domain.Entities;

namespace PlanifyIdentity.Database;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Status> Status { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>(b => b.ToTable(name: "Role"));
        builder.Entity<User>(b => b.ToTable("User"));
        builder.Entity<IdentityRoleClaim<string>>(b => b.ToTable(name: "RoleClaims"));
        builder.Entity<IdentityUserClaim<string>>(b => b.ToTable("UserClaim"));
        builder.Entity<IdentityUserLogin<string>>(b => b.ToTable("UserLogin"));
        builder.Entity<IdentityUserRole<string>>(b => b.ToTable("UserRole"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserToken"));
        builder.HasDefaultSchema("identity");
    }
}
