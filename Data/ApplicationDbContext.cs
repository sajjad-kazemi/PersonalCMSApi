using Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data
{
	public class ApplicationDbContext : DbContext
	{
		private readonly SqlConnection _sqlConnection;
		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options,
			IConfiguration configuration
			) : base(options)
		{
			_sqlConnection = new SqlConnection(configuration.GetConnectionString("CMS"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach(var entityType in modelBuilder.Model.GetEntityTypes()
						.Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
			{
				modelBuilder.Entity(entityType.Name).Property<byte[]>("RowVersion").IsRowVersion();
			}
		}


		public DbSet<User> Users { get; set; }
		public DbSet<Content> Contents { get; set; }
	}
}
