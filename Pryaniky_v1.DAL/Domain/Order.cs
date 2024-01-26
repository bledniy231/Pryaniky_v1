namespace Pryaniky_v1.DAL.Domain
{
	public class Order
	{
		public long Id { get; set; }
		public string CustomerName { get; set; }
		public long CustomerPhone { get; set; }
		public DateTime OrderDateTime { get; set; }
		public List<OrderItem> OrderItems { get; set; }
	}
}
