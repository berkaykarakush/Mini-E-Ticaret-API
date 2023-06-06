using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketsItemsQueryRequest: IRequest<List<GetBasketsItemsQueryResponse>>
    {
    }
}