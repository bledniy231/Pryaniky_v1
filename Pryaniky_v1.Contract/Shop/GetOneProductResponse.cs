using Pryaniky_v1.Contract.Default;
using Pryaniky_v1.Contract.Models;

namespace Pryaniky_v1.Contract.Shop
{
	public class GetOneProductResponse(ProductModel productModel, string? failedMessage) : DefaultResponse(failedMessage)
	{
		public ProductModel ProductModel { get; set; } = productModel;
	}
}
