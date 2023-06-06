namespace ETicaretAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketsItemsQueryResponse
    {
        public string BasketItemId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Qunatity { get; set; }
    }
}