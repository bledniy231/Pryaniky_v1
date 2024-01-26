using MediatR;
using Microsoft.EntityFrameworkCore;
using Pryaniky_v1.Contract.Default;
using Pryaniky_v1.Contract.Shop;
using Pryaniky_v1.DAL;

namespace Pryaniky_v1.BLL.Shop
{
	internal class DeleteOrderHandler(
		PryanikyDbContext dbContext) 
		: IRequestHandler<DeleteOrderRequest, DefaultResponse>
	{
		private readonly PryanikyDbContext _dbContext = dbContext;

		public async Task<DefaultResponse> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var orderFormDb = await _dbContext.Orders.FirstOrDefaultAsync(p => p.Id == request.OrderId);
				if (orderFormDb == null)
				{
					return new DefaultResponse($"Order with id {request.OrderId} not found");
				}

				_dbContext.Orders.Remove(orderFormDb);
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
