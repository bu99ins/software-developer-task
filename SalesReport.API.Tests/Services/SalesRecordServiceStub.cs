using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesReport.API.ClientObjects;
using SalesReport.API.Domain.Models;
using SalesReport.API.Domain.Services;

namespace SalesReport.API.Tests.Services
{
	public class SalesRecordServiceStub : ISalesRecordService
	{
		private int TotalRecords { get; }

		public SalesRecordServiceStub( int totalRecords )
		{
			TotalRecords = totalRecords;
		}

		public Task<IEnumerable<SalesRecord>> ListAsync( int start = 0, int limit = 20 )
		{
			return Task.FromResult( new List<SalesRecord>().AsEnumerable() );
		}

		public Task<SalesRecord> GetByIdAsync( int id )
		{
			return id <= TotalRecords
				? Task.FromResult( new SalesRecord { Id = id } )
				: Task.FromResult<SalesRecord>( null );
		}

		public Task<int> InsertAsync( IEnumerable<SalesRecord> records )
		{
			return Task.FromResult( records.Count() );
		}

		public Task<SalesRecord> UpdateAsync( SalesRecord record, SalesRecordUpdateObject update )
		{
			update.ApplyTo( record );
			return Task.FromResult( record  );
		}

		public Task DeleteAsync( SalesRecord record )
		{
			return Task.CompletedTask;
		}

		public void Dispose()
		{
		}
	}
}