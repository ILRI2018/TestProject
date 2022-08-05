using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PointService.DataAccess;
using PointService.DataAccess.Interfaces;
using PointService.DataAccess.Repositories;
using PointService.DataAccess.Uow;

namespace PointService.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PointService.API", Version = "v1" });
            });
            services.AddDbContext<PoinServiceContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(PoinServiceContext).Assembly.FullName)));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUow,Uow>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PointService.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
