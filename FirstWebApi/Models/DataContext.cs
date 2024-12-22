using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Models
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options) // this data context was used for in memory database usage. should I switch between contexts for testing ?
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                    .HasOne(n => n.Notebook)
                    .WithMany(e => e.Notes)
                    .HasForeignKey(e => e.NotebookId);
            modelBuilder.Entity<Notebook>()
                .HasOne(e => e.Room)
                .WithMany(e => e.Notebooks)
                .HasPrincipalKey(e => e.UniqueKey);
        }
    }
}
