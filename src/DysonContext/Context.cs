using DysonContext.Interface;
using DysonContext.TypeConfiguration;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics.CodeAnalysis;

namespace DysonContext;

public class Context(DbContextOptions<Context> options) : DbContext(options), IContext
{
    public DbSet<Entities.FileNodeEntity> FileNodes { get; set; } = null!;
    public DbSet<Entities.UserEntity> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString
            = Environment.GetEnvironmentVariable("DYSON_DATABASE_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=DYSON;Username=postgres;Password=postgres123;";
        _ = optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FileNodeTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => base.SaveChangesAsync(cancellationToken);

    public override DbSet<TEntity> Set<[DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicConstructors
        | DynamicallyAccessedMemberTypes.NonPublicConstructors
        | DynamicallyAccessedMemberTypes.PublicFields
        | DynamicallyAccessedMemberTypes.NonPublicFields
        | DynamicallyAccessedMemberTypes.PublicProperties
        | DynamicallyAccessedMemberTypes.NonPublicProperties
        | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>()
    {
        return base.Set<TEntity>();
    }
}
