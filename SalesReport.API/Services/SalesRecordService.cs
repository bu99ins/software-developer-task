using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SalesReport.API.ClientObjects;
using SalesReport.API.Domain.Models;
using SalesReport.API.Domain.Repositories;
using SalesReport.API.Domain.Services;

namespace SalesReport.API.Services
{
	public class SalesRecordService : ISalesRecordService
	{
		private readonly ISalesRecordRepository _salesRecordRepository;

		public SalesRecordService( ISalesRecordRepository salesRecordRepository )
		{
			_salesRecordRepository = salesRecordRepository;
		}

		public static ISalesRecordService Create( IdentityFactoryOptions<ISalesRecordService> options,
			IOwinContext context  )
		{
			return new SalesRecordService( context.Get<ISalesRecordRepository>() );
		}

		public async Task<IEnumerable<SalesRecord>> ListAsync( int start = 0, int limit = 20 )
		{
			return await _salesRecordRepository.ListAsync( start, limit );
		}

		public async Task<SalesRecord> GetByIdAsync( int id )
		{
			return await _salesRecordRepository.GetByIdAsync( id );
		}

		public async Task<int> InsertAsync( IEnumerable<SalesRecord> records )
		{
			var count = _salesRecordRepository.AddRange( records );
			await _salesRecordRepository.CompleteAsync();

			return count;
		}

		public async Task<SalesRecord> UpdateAsync( SalesRecord record, SalesRecordUpdateObject update )
		{
			_salesRecordRepository.Update( record, update );
			await _salesRecordRepository.CompleteAsync();

			return await _salesRecordRepository.GetByIdAsync( record.Id );
		}

		public Task DeleteAsync( SalesRecord record )
		{
			_salesRecordRepository.Remove( record );
			return _salesRecordRepository.CompleteAsync();

		}

		public void Dispose()
		{
			_salesRecordRepository.Dispose();
		}
	}
}