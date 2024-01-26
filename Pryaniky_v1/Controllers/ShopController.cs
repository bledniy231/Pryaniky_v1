using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pryaniky_v1.Contract.Default;
using Pryaniky_v1.Contract.Shop;

namespace Pryaniky_v1.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ShopController(IMediator mediator) : Controller
	{
		private readonly IMediator _mediator = mediator;

		[HttpPost]
		public async Task<ActionResult<AddProductResponse>> AddProduct(AddProductRequest request)
		{
			return await SendRequet<AddProductRequest, AddProductResponse>(request);
		}

		[HttpDelete]
		public async Task<ActionResult<DefaultResponse>> DeleteProduct([FromQuery] long productId)
		{
			return await SendRequet<DeleteProductRequest, DefaultResponse>(new DeleteProductRequest(productId));
		}

		[HttpGet]
		public async Task<ActionResult<GetOneProductResponse>> GetOneProduct([FromQuery] long productId)
		{
			return await SendRequet<GetOneProductRequest, GetOneProductResponse>(new GetOneProductRequest(productId));
		}

		[HttpGet]
		public async Task<ActionResult<GetProductsResponse>> GetProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
		{
			return await SendRequet<GetProductsRequest, GetProductsResponse>(new GetProductsRequest(pageNumber, pageSize));
		}

		[HttpPost]
		public async Task<ActionResult<MakeOrderResponse>> MakeOrder(MakeOrderRequest request)
		{
			return await SendRequet<MakeOrderRequest, MakeOrderResponse>(request);
		}

		[HttpDelete]
		public async Task<ActionResult<DefaultResponse>> DeleteOrder(DeleteOrderRequest request)
		{
			return await SendRequet<DeleteOrderRequest, DefaultResponse>(request);
		}

		private async Task<ActionResult<TResponse>> SendRequet<TRequest, TResponse>(TRequest request)
			where TRequest : IRequest<TResponse>
			where TResponse : DefaultResponse
		{
			var response = await _mediator.Send(request);
			if (response.FailedMessage != null)
			{
				return BadRequest(response.FailedMessage);
			}

			return Ok(response);
		}
	}
}
