namespace Pryaniky_v1.Contract.Default
{
	public class DefaultResponse(string? failedMessage)
	{
		/// <summary>
		/// If product was added successfully, this field will be null
		/// </summary>
		public string? FailedMessage { get; set; } = failedMessage;
	}
}
