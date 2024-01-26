namespace Pryaniky_v1.Contract.Models
{
	public class PageModel<TItem>(IEnumerable<TItem> items, int count, int pageNumber, int pageSize)
		where TItem : class
	{
		public IEnumerable<TItem> Items { get; set; } = items;

		public int PageNumber { get; private set; } = pageNumber;
		public int TotalPages { get; private set; } = (int)Math.Ceiling(count / (double)pageSize);

		public bool HasPreviousPage { get => PageNumber > 1; }
		public bool HasNextPage { get => PageNumber < TotalPages; }
	}
}
