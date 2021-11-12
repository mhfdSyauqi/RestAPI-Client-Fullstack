using EmployeeAPI.Context;
using EmployeeAPI.Repository;
using EmployeeAPI.Repository.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI
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
			services.AddControllers()
				.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); // error handle lazyloading
			services.AddScoped<AccountRepository>();
			services.AddScoped<EmployeeRepository>();
			services.AddScoped<EducationRepository>();
			services.AddScoped<ProfilingRepository>();
			services.AddScoped<UniversityRepository>();
			services.AddDbContext<MyContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("APIContext"))
			);
			// CORS
			//services.AddCors(c =>
			//{
			//	c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			//});

			// JWT
			services.AddAuthentication(auth =>
		 {
			 auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			 auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		 }).AddJwtBearer(option => {
			 option.RequireHttpsMetadata = false;
			 option.SaveToken = true;
			 option.TokenValidationParameters = new TokenValidationParameters()
			 {
				 ValidateIssuer = true,
				 ValidateAudience = false,
				 ValidAudience = Configuration["Jwt:Audience"],
				 ValidIssuer = Configuration["Jwt:Issuer"],
				 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
				 ValidateLifetime = true,
				 ClockSkew = TimeSpan.Zero
			 };
		 });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			//Cors
			app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

			app.UseHttpsRedirection();

			app.UseRouting();

			// JWT
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
