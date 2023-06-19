using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateStockQRCodeToProduct
{
    public class UpdateStockQRCodeToProductCommandRequest: IRequest<UpdateStockQRCodeToProductCommandResponse>
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}