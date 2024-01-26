using MediatR;
using Microsoft.EntityFrameworkCore;
using Pryaniky_v1.Contract.Shop;
using Pryaniky_v1.DAL;
using Pryaniky_v1.DAL.Domain;

namespace Pryaniky_v1.BLL.Shop
{
	internal class MakeOrderHandler(
		PryanikyDbContext dbContext) 
		: IRequestHandler<MakeOrderRequest, MakeOrderResponse>
	{
		private readonly PryanikyDbContext _dbContext = dbContext;

		public async Task<MakeOrderResponse> Handle(MakeOrderRequest request, CancellationToken cancellationToken)
		{
			if (request.OrderDetails == null)
			{
				return new MakeOrderResponse(-1, "No products for order in request");
			}

			var neededProductsFromDb = await _dbContext.Products
				.Where(p => request.OrderDetails.Keys.Contains(p.Id))
				.ToListAsync(cancellationToken);

			var response = new MakeOrderResponse(0, null);
			List<OrderItem> orderItems = [];

			// Using foreach only because break-operator
			foreach (var product in neededProductsFromDb)
			{
				if (request.OrderDetails[product.Id] > product.Quantity)
				{
					response.FailedMessage = "One or more products are being requested in quantities exceeding the available stock";
					break;
				}

				orderItems.Add(new OrderItem
				{
					Product = product,
					OrderedQuantity = request.OrderDetails[product.Id],
					OrderedPrice = product.Price * request.OrderDetails[product.Id]
				});
			}

			if (response.FailedMessage != null)
			{
				return response;
			}

			foreach (var product in neededProductsFromDb)
			{
				product.Quantity -= request.OrderDetails[product.Id];
			}

			var newOrder = new Order
			{
				CustomerName = request.CustomerName,
				CustomerPhone = request.CustomerPhone,
				OrderDateTime = DateTime.UtcNow,
				OrderItems = orderItems
			};

			_dbContext.Orders.Add(newOrder);
			try
			{
				await _dbContext.SaveChangesAsync(cancellationToken);
			}
			catch(Exception ex)
			{
				return new MakeOrderResponse(-1, $"Failed to save new record in database, exception message: {ex.Message}");
			}

			response = new MakeOrderResponse(newOrder.Id, null);

			return response;
		}
	}
}
