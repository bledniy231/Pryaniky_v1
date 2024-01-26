using Pryaniky_v1.Contract.Default;

namespace Pryaniky_v1.Contract.Shop
{
	public class AddProductResponse(long productId, string? failedMessage) : DefaultResponse(failedMessage)
	{
		public AddProductResponse() : this(0, null) { }

		public long ProductId { get; set; } = productId;
	}
}
