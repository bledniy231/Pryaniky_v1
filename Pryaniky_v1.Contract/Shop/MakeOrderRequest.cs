using MediatR;

namespace Pryaniky_v1.Contract.Shop
{
	public class MakeOrderRequest : IRequest<MakeOrderResponse>
	{
		public string CustomerName { get; set; }
		public long CustomerPhone { get; set; }
		/// <summary>
		/// Dict represents KeyValuePairs where the key is the ProductId and the value is the quantity of this product that the customer wants to buy
		/// </summary>
		public Dictionary<long, int> OrderDetails { get; set; }
	}
}
