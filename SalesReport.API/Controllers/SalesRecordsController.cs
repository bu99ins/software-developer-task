using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using SalesReport.API.ClientObjects;
using SalesReport.API.Domain.Models;
using SalesReport.API.Domain.Services;

namespace SalesReport.API.Controllers
{
	public class SalesRecordsController : ApiController
	{
		private ISalesRecordService _salesRecordServiceService;

		public SalesRecordsController()
		{
		}

		public SalesRecordsController( ISalesRecordService salesRecordService )
		{
			SalesRecordService = salesRecordService;
		}

		public ISalesRecordService SalesRecordService
		{
			get => _salesRecordServiceService ?? HttpContext.Current.GetOwinContext().Get<ISalesRecordService>();
			private set => _salesRecordServiceService = value;
		}

		// GET api/<controller>
		[HttpGet]
		public async Task<IEnumerable<SalesRecord>> GetAllAsync( int start = 0, int limit = 20 )
		{
			var records = await SalesRecordService.ListAsync( start, limit );
			return records;
		}

		// GET api/<controller>/5
		[HttpGet]
		public async Task<SalesRecord> GetAsync( int id )
		{
			return await SalesRecordService.GetByIdAsync( id );
		}

		// POST api/<controller>
		[HttpPost]
		public async Task<IHttpActionResult> PostAsync()
		{
			if (!Request.Content.IsMimeMultipartContent())
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType); 

			var provider = new MultipartMemoryStreamProvider();
			await Request.Content.ReadAsMultipartAsync(provider);

			var count = 0;

			foreach (var file in provider.Contents)
			{
				var buffer = await file.ReadAsStringAsync();
				//ToDo: Rewrite to stream parsing to allow larger content.

				var records = buffer
					.Split( new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None )
					.Skip( 1 )
					.Where( s => !string.IsNullOrWhiteSpace( s ) )
					.Select( s => s.Split( ',' ) )
					.Where( a => a.Length == 14 )
					.Select( MakeRecordFromRow )
					.Where( sr => sr != null );

				count += await SalesRecordService.InsertAsync( records );
			}

			return Ok( count );
		}

		private static SalesRecord MakeRecordFromRow( string[] a )
		{
			try
			{
				return new SalesRecord
				{
					Region = a[0],
					Country = a[1],
					ItemType = a[2],
					SalesChannel = a[3],
					OrderPriority = a[4],
					OrderDate = DateTime.ParseExact( a[5], "M/d/yyyy", CultureInfo.InvariantCulture),
					OrderId = int.Parse( a[6] ),
					ShipDate = DateTime.ParseExact( a[7], "M/d/yyyy", CultureInfo.InvariantCulture ),
					UnitsSold = int.Parse( a[8] ),
					UnitPrice = decimal.Parse( a[9], CultureInfo.InvariantCulture ),
					UnitCost = decimal.Parse( a[10], CultureInfo.InvariantCulture ),
					TotalRevenue = decimal.Parse( a[11], CultureInfo.InvariantCulture ),
					TotalCost = decimal.Parse( a[12], CultureInfo.InvariantCulture ),
					TotalProfit = decimal.Parse( a[13], CultureInfo.InvariantCulture )
				};
			}
			catch( Exception e )
			{
				Console.WriteLine( e );
				// ToDo: Configure file logger to analyze future problems.
			}

			return null;
		}

		// PUT api/<controller>/5
		// [Authorize]
		[HttpPut]
		public async Task<IHttpActionResult> PutAsync( int id, [FromBody] SalesRecordUpdateObject value )
		{
			if( !ModelState.IsValid ) return BadRequest( ModelState );

			if( value.Id != id ) return BadRequest( "Wrong update data." );

			var record = await SalesRecordService.GetByIdAsync( id );
			if( record == null ) return BadRequest( "The sales record does not exist." );

			var result = await SalesRecordService.UpdateAsync( record, value );

			return result != null ? (IHttpActionResult) Ok( result ) : BadRequest( "Internal server error." );
		}

		// DELETE api/<controller>/5
		// [Authorize]
		[HttpDelete]
		public async Task<IHttpActionResult> DeleteAsync( int id )
		{
			var record = await SalesRecordService.GetByIdAsync( id );
			if( record == null ) return BadRequest( "The sales record does not exist." );

			await SalesRecordService.DeleteAsync( record );

			return Ok( record );
		}

	}
}