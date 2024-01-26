using MediatR;
using Pryaniky_v1.Contract.Default;

namespace Pryaniky_v1.Contract.Shop
{
	public class DeleteProductRequest(long productId) : IRequest<DefaultResponse>
	{
		public long ProductId { get; set; } = productId;
	}
}
