using MediatR;
using Microsoft.EntityFrameworkCore;
using Pryaniky_v1.Contract.Models;
using Pryaniky_v1.Contract.Shop;
using Pryaniky_v1.DAL;

namespace Pryaniky_v1.BLL.Shop
{
	internal class GetProductsHandler(
		PryanikyDbContext dbContext) 
		: IRequestHandler<GetProductsRequest, GetProductsResponse>
	{
		private readonly PryanikyDbContext _dbContext = dbContext;

		public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
		{
			var source = _dbContext.Products.AsNoTracking().AsQueryable();
			int totalCount = 0;
			try
			{
				totalCount = await source.CountAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				return new GetProductsResponse(null, ex.Message);
			}

			var products = await source
				.Skip((request.PageNumber - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync(cancellationToken);

			var productsModels = products.Select(p => new ProductModel
			{
				Id = p.Id,
				Price = p.Price,
				ProductName = p.ProductName,
				Quantity = p.Quantity
			});

			var pagedProducts = new PageModel<ProductModel>(productsModels, totalCount, request.PageNumber, request.PageSize);

			var response = new GetProductsResponse(pagedProducts, null);

			return response;
		}
	}
}
