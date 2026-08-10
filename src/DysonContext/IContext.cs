using Microsoft.EntityFrameworkCore;

using System.Diagnostics.CodeAnalysis;

namespace DysonContext;

public interface IContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<TEntity> Set<[DynamicallyAccessedMembers(
                                           DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties |
                                           DynamicallyAccessedMemberTypes.Interfaces)] TEntity>() where TEntity : class;

        DbSet<Entities.FileNodeEntity> FileNodes { get; set; }
}
