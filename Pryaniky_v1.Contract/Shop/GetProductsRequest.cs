using MediatR;

namespace Pryaniky_v1.Contract.Shop
{
	public class GetProductsRequest(int pageNumber, int pageSize) : IRequest<GetProductsResponse>
	{
		public int PageNumber { get; set; } = pageNumber;
		public int PageSize { get; set; } = pageSize;
	}
}
