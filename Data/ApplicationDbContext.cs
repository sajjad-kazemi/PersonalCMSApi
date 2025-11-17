using Data.Entities;
using Data.Migrations;
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

		public override int SaveChanges()
		{
			var entries = ChangeTracker
				.Entries().Where(e => e.Entity is BaseEntity);

			foreach (var entityEntry in entries)
			{
				var entity = (BaseEntity)entityEntry.Entity;
				if (entityEntry.State == EntityState.Added)
				{
					var now = DateTime.UtcNow;
					entity.CreatedDate = now;
				}
				if (entityEntry.State == EntityState.Modified)
				{
					var now = DateTime.UtcNow;
					entity.ModifyDate = now;
				}
			}

			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker
				.Entries().Where(e => e.Entity is BaseEntity);

			foreach (var entityEntry in entries)
			{
				var entity = (BaseEntity)entityEntry.Entity;
				if (entityEntry.State == EntityState.Added)
				{
					var now = DateTime.UtcNow;
					entity.CreatedDate = now;
					entity.ModifyDate = now;
				}
				if (entityEntry.State == EntityState.Modified)
				{
					var now = DateTime.UtcNow;
					entity.ModifyDate = now;
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Content> Contents { get; set; }
	}
}
