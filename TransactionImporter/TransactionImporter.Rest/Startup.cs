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
using TransactionImporter.Services;

namespace TransactionImporter.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
