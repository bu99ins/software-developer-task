using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesReport.API.ClientObjects;
using SalesReport.API.Domain.Models;

namespace SalesReport.API.Domain.Repositories
{
	public interface ISalesRecordRepository : IDisposable
	{
		Task<IEnumerable<SalesRecord>> ListAsync( int start = 0, int limit = 20 );
		Task<SalesRecord> GetByIdAsync( int id );
		Task CompleteAsync();
		int AddRange( IEnumerable<SalesRecord> records );
		void Update( SalesRecord record, SalesRecordUpdateObject update );
		void Remove( SalesRecord record );
	}
}
