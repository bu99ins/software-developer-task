using SalesReport.API.Persistence.Contexts;

namespace SalesReport.API.Persistence.Repositories
{
	public abstract class BaseRepository
	{
		protected AppDbContext Context { get; }

		protected BaseRepository( AppDbContext context )
		{
			Context = context;
		}
	}
}