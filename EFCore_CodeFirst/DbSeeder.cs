using EFCore_CodeFirst.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_CodeFirst
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstrumentType>().HasData(
                new InstrumentType { InstrumentTypeId = 1, Name = "Acoustic Guitar" },
                new InstrumentType { InstrumentTypeId = 2, Name = "Electric Guitar" },
                new InstrumentType { InstrumentTypeId = 3, Name = "Classic Guitar" },
                new InstrumentType { InstrumentTypeId = 4, Name = "Drums" },
                new InstrumentType { InstrumentTypeId = 5, Name = "Bass" },
                new InstrumentType { InstrumentTypeId = 6, Name = "Keyboard" }
                );
        }
    }
}
