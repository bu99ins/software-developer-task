using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesReport.API.Controllers;
using SalesReport.API.Tests.Services;

namespace SalesReport.API.Tests
{
	[TestClass]
	public class SalesRecordsDeleteTest
	{
		[TestMethod]
		public async Task ReturnsBadRequestIfNotFound()
		{
			var controller = new SalesRecordsController( new SalesRecordServiceStub( 20 ) );

			var result = await controller.DeleteAsync( 21 );

			Assert.IsInstanceOfType( result, typeof( BadRequestErrorMessageResult ) );
		}
	}
}
