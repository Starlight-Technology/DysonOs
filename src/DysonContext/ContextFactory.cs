using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DysonContext;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Context>();

        // Usa a mesma connection string que você definiu
        string? connectionString
            = Environment.GetEnvironmentVariable("DYSON_DATABASE_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=DYSON;Username=postgres;Password=postgres123;";

        optionsBuilder.UseNpgsql(connectionString);

        return new Context(optionsBuilder.Options);
    }
}
