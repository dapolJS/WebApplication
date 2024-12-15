using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Models
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }
    }
}
