using Microsoft.EntityFrameworkCore;

namespace Tan.Infrastructure.DbContext;


public class SampleContext(DbContextOptions<SampleContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Domain.Entities.Sample> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CustomerModelBuilder(modelBuilder);
    }

    private static void CustomerModelBuilder(ModelBuilder modelBuilder)
    {
        var model = modelBuilder.Entity<Domain.Entities.Sample>();

        model.ToTable("Sample");

        model.HasIndex(x => x.Description)
            .IsUnique();

        model.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        model.Property(x => x.Description)
            .HasMaxLength(100)
            .IsRequired();
    }
}
