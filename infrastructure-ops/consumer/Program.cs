using consumer.DataAccess.PostgreSQL;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace consumer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = FunctionsApplication.CreateBuilder(args);
            builder.Services.AddDbContextPool<ContractDBContext>(opt =>
            {
                opt.UseNpgsql(builder.Configuration.GetSection("PostgreSQLConnectionString").Value);
            });

            builder.Services.AddScoped<IContractRepository,ContractRepository>();

            builder.Build().Run();
        }
    }
}
