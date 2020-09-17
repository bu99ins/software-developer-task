using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SalesReport.API.ClientObjects;
using SalesReport.API.Domain.Models;
using SalesReport.API.Domain.Repositories;
using SalesReport.API.Persistence.Contexts;

namespace SalesReport.API.Persistence.Repositories
{
	public class SalesRecordRepository : BaseRepository, ISalesRecordRepository
	{
		public SalesRecordRepository( AppDbContext context ) : base( context )
		{
		}

		public static ISalesRecordRepository Create( IdentityFactoryOptions<ISalesRecordRepository> options,
			IOwinContext context  )
		{
			return new SalesRecordRepository( context.Get<AppDbContext>() );
		}

		public async Task<IEnumerable<SalesRecord>> ListAsync( int start = 0, int limit = 20 )
		{
			return await Context.SalesRecords
				.OrderBy( sr => sr.Id )
				.Skip( start )
				.Take( limit ).ToListAsync();
		}

		public async Task<SalesRecord> GetByIdAsync( int id )
		{
			return await Context.SalesRecords.FindAsync( id );
		}

		public async Task CompleteAsync()
		{
			await Context.SaveChangesAsync();
		}

		public int AddRange( IEnumerable<SalesRecord> records )
		{
			// ToDo: Try EF extensions for bulk operations, or search other ways to improve performance.
			return Context.SalesRecords.AddRange( records ).Count();
		}

		public void Update( SalesRecord record, SalesRecordUpdateObject update )
		{
			Context.SalesRecords.Attach( record );
			update.ApplyTo( record );
		}

		public void Remove( SalesRecord record )
		{
			Context.SalesRecords.Remove( record );
		}

		public void Dispose()
		{
		}
	}
}