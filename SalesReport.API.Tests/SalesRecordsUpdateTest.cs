using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesReport.API.ClientObjects;
using SalesReport.API.Controllers;
using SalesReport.API.Tests.Services;

namespace SalesReport.API.Tests
{
	[TestClass]
	public class SalesRecordsUpdateTest
	{
		[TestMethod]
		public async Task ReturnsBadRequestIfWrongId()
		{
			var controller = new SalesRecordsController( new SalesRecordServiceStub( 20 ) );

			var result = await controller.PutAsync( 1, new SalesRecordUpdateObject { Id = 2 } );

			Assert.IsInstanceOfType( result, typeof( BadRequestErrorMessageResult ) );
		}

		[TestMethod]
		public async Task ReturnsBadRequestIfNotFound()
		{
			var controller = new SalesRecordsController( new SalesRecordServiceStub( 20 ) );

			var result = await controller.PutAsync( 21, new SalesRecordUpdateObject { Id = 21 } );

			Assert.IsInstanceOfType( result, typeof( BadRequestErrorMessageResult ) );
		}
	}
}
