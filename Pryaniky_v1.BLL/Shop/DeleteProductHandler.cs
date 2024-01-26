using MediatR;
using Microsoft.EntityFrameworkCore;
using Pryaniky_v1.Contract.Default;
using Pryaniky_v1.Contract.Shop;
using Pryaniky_v1.DAL;

namespace Pryaniky_v1.BLL.Shop
{
	internal class DeleteProductHandler(
		PryanikyDbContext dbContext)
		: IRequestHandler<DeleteProductRequest, DefaultResponse>
	{
		private readonly PryanikyDbContext _dbContext = dbContext;

		public async Task<DefaultResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var productFormDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId);
				if (productFormDb == null)
				{
					return new DefaultResponse($"Product with id {request.ProductId} not found");
				}

				_dbContext.Products.Remove(productFormDb);
				await _dbContext.SaveChangesAsync(cancellationToken);
				return new DefaultResponse(null);
			}
			catch (Exception ex)
			{
				return new DefaultResponse($"Failed to delete record from database, excetion message: {ex.Message}");
			}
		}
	}
}
