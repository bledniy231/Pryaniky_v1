using Pryaniky_v1.Contract.Default;
using Pryaniky_v1.Contract.Models;

namespace Pryaniky_v1.Contract.Shop
{
	public class GetProductsResponse(PageModel<ProductModel> pagedProducts, string? failedMessage) : DefaultResponse(failedMessage)
	{
		public PageModel<ProductModel> PagedProducts { get; set; } = pagedProducts;
	}
}
