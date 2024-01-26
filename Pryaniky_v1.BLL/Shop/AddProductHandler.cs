using MediatR;
using Microsoft.EntityFrameworkCore;
using Pryaniky_v1.Contract.Shop;
using Pryaniky_v1.DAL;
using Pryaniky_v1.DAL.Domain;

namespace Pryaniky_v1.BLL.Shop
{
	internal class AddProductHandler(
		PryanikyDbContext dbContext) 
		: IRequestHandler<AddProductRequest, AddProductResponse>
	{
		private readonly PryanikyDbContext _dbContext = dbContext;

		public async Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
		{
			var response = new AddProductResponse();
			try
			{
				var productFromDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductName.Equals(request.ProductName), cancellationToken);
				if (productFromDb != null)
				{
					productFromDb.Quantity += request.Quantity;
					productFromDb.Price = request.Price;
					response.ProductId = productFromDb.Id;
					await _dbContext.SaveChangesAsync(cancellationToken);
				}
				else
				{
					var product = new Product
					{
						ProductName = request.ProductName,
						Price = request.Price,
						Quantity = request.Quantity
					};

					await _dbContext.Products.AddAsync(product, cancellationToken);
					await _dbContext.SaveChangesAsync(cancellationToken);
					response.ProductId = product.Id;
				}

				return response;
			}
			catch (Exception ex)
			{
				response.FailedMessage = $"Failed to add new product in database, exception message: {ex.Message}";
				return response;
			}
		}
	}
}
