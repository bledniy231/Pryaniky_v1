namespace Pryaniky_v1.DAL.Domain
{
	public class Product
	{
		public long Id { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public List<OrderItem> OrderItems { get; set; }
	}
}
