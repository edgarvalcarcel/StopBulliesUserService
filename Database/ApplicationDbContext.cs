using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StopBulliesUserService.Domain.Entities;

namespace StopBulliesUserService.Database;

internal sealed class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Status> Status { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        _ = builder.Entity<IdentityRole>(static b => b.ToTable(name: "Role"));
        _ = builder.Entity<User>(b => b.ToTable("User"));
        _ = builder.Entity<IdentityRoleClaim<string>>(b => b.ToTable(name: "RoleClaims"));
        _ = builder.Entity<IdentityUserClaim<string>>(b => b.ToTable("UserClaim"));
        _ = builder.Entity<IdentityUserLogin<string>>(b => b.ToTable("UserLogin"));
        _ = builder.Entity<IdentityUserRole<string>>(b => b.ToTable("UserRole"));
        _ = builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserToken"));
        _ = builder.HasDefaultSchema("identity");
    }
}
