using MediatR;
using Pryaniky_v1.Contract.Default;

namespace Pryaniky_v1.Contract.Shop
{
	public class AddProductRequest : IRequest<AddProductResponse>
	{
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
