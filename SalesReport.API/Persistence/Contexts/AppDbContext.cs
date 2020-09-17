using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SalesReport.API.Domain.Models;

namespace SalesReport.API.Persistence.Contexts
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<SalesRecord> SalesRecords { get; set; }

		public AppDbContext() : base( "DefaultConnection", false )
		{
		}

		public static AppDbContext Create()
		{
			return new AppDbContext();
		}

		protected override void OnModelCreating( DbModelBuilder builder )
		{
			base.OnModelCreating( builder );

			builder.Entity<SalesRecord>().ToTable( "SalesRecords" );

			builder.Entity<SalesRecord>().HasKey( p => p.Id );
			builder.Entity<SalesRecord>().Property( p => p.Id )
				.IsRequired()
				.HasDatabaseGeneratedOption( DatabaseGeneratedOption.Identity );
		}
	}
}