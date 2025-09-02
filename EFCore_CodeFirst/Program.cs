
using EFCore_CodeFirst.Formatter;
using EFCore_CodeFirst.Service;

namespace EFCore_CodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IPlayerService, PlayerService>();
            builder.Services.AddDatabaseService(builder.Configuration.GetConnectionString("CodeFirstDemoDb"));

            builder.Services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters().AddMvcOptions(c => c.OutputFormatters.Add(new CsvOutputFormatter()));

            //Configure JSON Serialization in ASP.NET Core
            //builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            //builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
