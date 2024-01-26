namespace Pryaniky_v1.DAL.Domain
{
	public class OrderItem
	{
		public long Id { get; set; }
		public long OrderId { get; set; }
		public long? ProductId { get; set; }
		public int OrderedQuantity { get; set; }
		/// <summary>
		/// OrderedPrice is needed in case when product price change, because we need to save old price.
		/// Represents the product of the price and the quantity of the item
		/// </summary>
		public double OrderedPrice { get; set; }
		public Order Order { get; set; }
		public Product? Product { get; set; }
	}
}
