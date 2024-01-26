using MediatR;
using Microsoft.EntityFrameworkCore;
using Pryaniky_v1.Contract.Shop;
using Pryaniky_v1.DAL;

namespace Pryaniky_v1.BLL.Shop
{
	internal class GetOneProductHandler(
		PryanikyDbContext dbContext) 
		: IRequestHandler<GetOneProductRequest, GetOneProductResponse>
	{
		private readonly PryanikyDbContext _dbContext = dbContext;

		public async Task<GetOneProductResponse> Handle(GetOneProductRequest request, CancellationToken cancellationToken)
		{
			var productFromDb = await _dbContext.Products
				.AsNoTracking()
				.FirstOrDefaultAsync(p => p.Id == request.ProductId);

			if (productFromDb == null)
			{
				return new GetOneProductResponse(null, $"Product with id {request.ProductId} not found");
			}

			return new GetOneProductResponse(new Contract.Models.ProductModel
			{
				Id = productFromDb.Id,
				ProductName = productFromDb.ProductName,
				Price = productFromDb.Price,
				Quantity = productFromDb.Quantity
			}, null);
		}
	}
}
