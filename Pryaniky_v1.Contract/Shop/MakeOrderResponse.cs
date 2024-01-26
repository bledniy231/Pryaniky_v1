using Pryaniky_v1.Contract.Default;

namespace Pryaniky_v1.Contract.Shop
{
	public class MakeOrderResponse(long orderId, string? failedMessage) : DefaultResponse(failedMessage)
	{
		public long OrderId { get; set; } = orderId;
	}
}
