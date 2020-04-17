using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TransactionImporter.DataAccess;
using TransactionImporter.Rest.Extensions;
using TransactionImporter.Rest.Mappings;
using TransactionImporter.Rest.Services;
using TransactionImporter.Services;

namespace TransactionImporter.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
         =>   Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // add mapping functionality
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile("default")));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<TransactionDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TransactionDb")));

            services.AddScoped<ITransactionReader, TransactionReader>();
            services.AddScoped<ITransactionImporter, TransactionImporterService>();
            services.AddSingleton<IFileParser, FileParser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
                app.UseGlobalExceptionHandler(logger, respondWithJsonErrorDetails: true);
            else
            {
                app.UseGlobalExceptionHandler(logger, respondWithJsonErrorDetails: false);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}