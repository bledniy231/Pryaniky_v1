using MediatR;
using Pryaniky_v1.Contract.Default;

namespace Pryaniky_v1.Contract.Shop
{
	public class DeleteOrderRequest : IRequest<DefaultResponse>
	{
		public long OrderId { get; set; }
	}
}
