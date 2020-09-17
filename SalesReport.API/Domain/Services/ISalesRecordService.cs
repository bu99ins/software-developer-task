using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesReport.API.ClientObjects;
using SalesReport.API.Domain.Models;

namespace SalesReport.API.Domain.Services
{
	public interface ISalesRecordService : IDisposable
	{
		Task<IEnumerable<SalesRecord>> ListAsync( int start = 0, int limit = 20 );
		Task<SalesRecord> GetByIdAsync( int id );
		Task<int> InsertAsync( IEnumerable<SalesRecord> records );
		Task<SalesRecord> UpdateAsync( SalesRecord record, SalesRecordUpdateObject update );
		Task DeleteAsync( SalesRecord record );
	}
}
