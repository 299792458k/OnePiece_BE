using Microsoft.EntityFrameworkCore;
using TreasureHunting.Model;

namespace TreasureHunting.DL.DBContext
{
    /// <summary>
    /// Database context for Treasure Hunting application
    /// Manages entity configurations and database operations using Entity Framework Core with SQLite
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// DbSet for TreasureMap entities
        /// Represents the TreasureMaps table in the database
        /// </summary>
        public DbSet<TreasureMap> TreasureMaps { get; set; }

        /// <summary>
        /// Constructor for AppDbContext
        /// </summary>
        /// <param name="options">Database context options configured with SQLite connection</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Configures the model and entity relationships
        /// Defines table mappings, primary keys, and property configurations
        /// </summary>
        /// <param name="modelBuilder">Model builder instance for entity configuration</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TreasureMap entity
            modelBuilder.Entity<TreasureMap>(entity =>
            {
                entity.ToTable("TreasureMaps");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });
        }
    }
}
