using Microsoft.Owin;
using Owin;
using SalesReport.API.Domain.Repositories;
using SalesReport.API.Domain.Services;
using SalesReport.API.Persistence.Repositories;
using SalesReport.API.Services;

[assembly: OwinStartup( typeof( SalesReport.API.Startup ) )]

namespace SalesReport.API
{
	public partial class Startup
	{
		public void Configuration( IAppBuilder app )
		{
			ConfigureAuth( app );

			// ToDo: Install and configure some dedicated DI manager to register domain dependencies.
			app.CreatePerOwinContext<ISalesRecordRepository>( SalesRecordRepository.Create );
			app.CreatePerOwinContext<ISalesRecordService>( SalesRecordService.Create );
		}
	}
}
