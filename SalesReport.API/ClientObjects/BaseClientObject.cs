namespace SalesReport.API.ClientObjects
{
	public abstract class BaseClientObject<T>
	{
		public virtual void InitFrom( T obj )
		{
		}

		public virtual void ApplyTo( T obj )
		{
		}
	}
}