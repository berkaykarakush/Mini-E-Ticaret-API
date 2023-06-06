using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketsItemsQueryHandler : IRequestHandler<GetBasketsItemsQueryRequest, List<GetBasketsItemsQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetBasketsItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketsItemsQueryResponse>> Handle(GetBasketsItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemsAsync();
            return basketItems.Select(ba => new GetBasketsItemsQueryResponse
            {
                BasketItemId = ba.Id.ToString(),
                Name = ba.Product.Name,
                Price = ba.Product.Price,
                Qunatity = ba.Quantity
            }).ToList();

        }
    }
}
