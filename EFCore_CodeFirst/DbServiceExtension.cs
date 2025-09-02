using Microsoft.EntityFrameworkCore;

namespace EFCore_CodeFirst
{
    public static class DbServiceExtension
    {
        public static void AddDatabaseService(this IServiceCollection services, string connectionString)
        => services.AddDbContext<CodeFirstDemoContext>(options => options.UseSqlServer(connectionString));
    }
}
