using MediatR;
using Pryaniky_v1.Contract.Models;

namespace Pryaniky_v1.Contract.Shop
{
	public class GetOneProductRequest(long productId) : IRequest<GetOneProductResponse>
	{
		public long ProductId { get; set; } = productId;
	}
}
