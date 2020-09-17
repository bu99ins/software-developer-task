using System;
using System.ComponentModel.DataAnnotations;
using SalesReport.API.Domain.Models;

namespace SalesReport.API.ClientObjects
{
	public class SalesRecordUpdateObject : BaseClientObject<SalesRecord>
	{
		[Required]
		[Range(1, int.MaxValue)]
		public int Id { get; set; }

		public string Region { get; set; }
		public string Country { get; set; }
		public string ItemType { get; set; }
		public string SalesChannel { get; set; }
		public string OrderPriority { get; set; }
		public DateTime? OrderDate { get; set; }
		public int OrderId { get; set; }
		public DateTime? ShipDate { get; set; }
		public int UnitsSold { get; set; }
		public decimal? UnitPrice { get; set; }
		public decimal? UnitCost { get; set; }
		public decimal? TotalRevenue { get; set; }
		public decimal? TotalCost { get; set; }
		public decimal? TotalProfit { get; set; }

		public override void InitFrom( SalesRecord record )
		{
			base.InitFrom( record );

			if( Id <= 0 ) Id = record.Id;

			Region = record.Region;
			Country = record.Country;
			ItemType = record.ItemType;
			SalesChannel = record.SalesChannel;
			OrderPriority = record.OrderPriority;
			OrderDate = record.OrderDate;
			OrderId = record.OrderId;
			ShipDate = record.ShipDate;
			UnitsSold = record.UnitsSold;
			UnitPrice = record.UnitPrice;
			UnitCost = record.UnitCost;
			TotalRevenue = record.TotalRevenue;
			TotalCost = record.TotalCost;
			TotalProfit = record.TotalProfit;
		}

		public override void ApplyTo( SalesRecord record )
		{
			base.ApplyTo( record );

			if( record.Id <= 0 ) record.Id = Id;

			if( Region != null ) record.Region = Region;
			if( Country != null ) record.Country = Country;
			if( ItemType != null ) record.ItemType = ItemType;
			if( SalesChannel != null ) record.SalesChannel = SalesChannel;
			if( OrderPriority != null ) record.OrderPriority = OrderPriority;
			if( OrderDate != null ) record.OrderDate = OrderDate.Value;
			if( OrderId > 0 ) record.OrderId = OrderId;
			if( ShipDate != null ) record.ShipDate = ShipDate.Value;
			if( UnitsSold > 0 ) record.UnitsSold = UnitsSold;
			if( UnitPrice != null ) record.UnitPrice = UnitPrice.Value;
			if( UnitCost != null ) record.UnitCost = UnitCost.Value;
			if( TotalRevenue != null ) record.TotalRevenue = TotalRevenue.Value;
			if( TotalCost != null ) record.TotalCost = TotalCost.Value;
			if( TotalProfit != null ) record.TotalProfit = TotalProfit.Value;
		}
	}
}