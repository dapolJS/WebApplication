using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Models
{
    public class DataContext(DbContextOptions options) : DbContext(options)
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
