using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Models;

public class DataContextEF(DbContextOptions<DataContextEF> options) : DbContext(options)
{
    public DbSet<Note> Note { get; set; }

    public DataContextEF()
        : this(new DbContextOptions<DataContextEF>())
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer(
                "Server=localhost;Database=FirstWebApiNotes;TrustServerCertificate=true;Trusted_Connection=true;",
                options => options.EnableRetryOnFailure()
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("NotesAppSchema");

        modelBuilder.Entity<Note>()
            .Property(n => n.Date)
            .HasDefaultValueSql("GETUTCDATE()");
        
    }
}