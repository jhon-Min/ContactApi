using Microsoft.EntityFrameworkCore;

namespace ContactApi.Models {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Contact>? Contacts { get; set; } 
    }
}