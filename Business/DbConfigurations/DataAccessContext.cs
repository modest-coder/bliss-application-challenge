using Microsoft.EntityFrameworkCore;
using Business.Model;

namespace Business.DbConfigurations
{
    public class DataAccessContext : DbContext
    {
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Option> Options { get; set; }

        //public DataAccessContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=\"C:/challenge-data/database.db\"");
    }
}
