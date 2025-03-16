using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Models;

public class DataContextEF(DbContextOptions<DataContextEF> options) : DbContext(options)
{
    public DbSet<Note> Note { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<Notebook> Notebook { get; set; }

    // Parameterless constructor (for testing or DI purposes)
    public DataContextEF()
        : this(new DbContextOptions<DataContextEF>()) { }

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

        modelBuilder
            .Entity<Note>()
            .HasOne(n => n.Notebook)
            .WithMany(e => e.Notes)
            .HasForeignKey(e => e.NotebookId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder
            .Entity<Notebook>()
            .HasOne(e => e.Room)
            .WithMany(e => e.Notebooks)
            .HasPrincipalKey(e => e.UniqueKey);
    }
}
