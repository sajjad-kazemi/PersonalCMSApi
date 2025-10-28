using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services ,
			WebApplicationBuilder builder)
		{
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddResponseCompression(options =>
			{
				options.EnableForHttps = true;
				options.Providers.Add<GzipCompressionProvider>();
			});
			services.Configure<KestrelServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});
			services.Configure<GzipCompressionProviderOptions>(options =>
			{
				options.Level = System.IO.Compression.CompressionLevel.Fastest;
			});
			var connectionString = builder.Configuration.GetConnectionString("CMS");
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(connectionString ,
					serverDbContextOptionsBuilder =>
					{
						var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
						serverDbContextOptionsBuilder.CommandTimeout(minutes);
					});
			});

			// add services here
			//services.AddScoped<>();

			services.AddScoped<IUserService , UserService>();

			return services;
		}
	}
}
