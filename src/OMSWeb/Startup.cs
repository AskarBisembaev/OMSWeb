using System;
using System.IO;
using OMS.Data.Access.DAL;
using OMSWeb.Filters;
using OMSWeb.IoC;
using OMS.Security;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using OMSWeb.Data.Access.DAL;
using Microsoft.OpenApi.Models;
using OMSWeb.Configurations;
using OMSWeb.Infrastructure;
using Hangfire;
using OMSWeb.Infrastructure.Repositories;

namespace OMSWeb
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			string connection = Configuration.GetConnectionString("OMSDatabase");

			services.AddDbContext<dbcontext>(options => options.UseSqlServer(connection));

			services.Configure<CacheConfiguration>(Configuration.GetSection("CacheConfiguration"));
			services.AddMemoryCache();
			services.AddTransient<MemoryCacheService>();
			services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
			services.AddHangfireServer();
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "OMSWebMini", Version = "v1" });
			});

			services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddTransient<ICategoryRepository, CategoryRepository>();

		}



		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "OMSWebMini");
			});


			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
