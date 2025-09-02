using EFCore_CodeFirst.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_CodeFirst
{
    public class CodeFirstDemoContext : DbContext
    {
        public CodeFirstDemoContext(DbContextOptions<CodeFirstDemoContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<InstrumentType> InstrumentTypes { get; set; }
        public DbSet<PlayerInstrument> PlayerInstruments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Player -> PlayerInstrument
            modelBuilder.Entity<PlayerInstrument>()
                .HasOne(pi => pi.Player)
                .WithMany(p => p.Instruments)
                .HasForeignKey(pi => pi.PlayerId);

            // InstrumentType -> PlayerInstrument
            modelBuilder.Entity<PlayerInstrument>()
                .HasOne(pi => pi.InstrumentType)
                .WithMany(it => it.PlayerInstruments)
                .HasForeignKey(pi => pi.InstrumentTypeId);

            modelBuilder.Seed();
        }
    }
}
