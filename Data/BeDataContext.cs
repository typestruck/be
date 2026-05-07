using Microsoft.EntityFrameworkCore;

namespace be.Data;

public class BeDataContext(DbContextOptions<BeDataContext> options) : DbContext(options)
{
    public DbSet<be.Models.User> User { get; set; } = default!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();
}
