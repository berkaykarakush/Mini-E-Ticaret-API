using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateStockQRCodeToProduct
{
    public class UpdateStockQRCodeToProductCommandHandler : IRequestHandler<UpdateStockQRCodeToProductCommandRequest, UpdateStockQRCodeToProductCommandResponse>
    {
        readonly IProductService _productService;

        public UpdateStockQRCodeToProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateStockQRCodeToProductCommandResponse> Handle(UpdateStockQRCodeToProductCommandRequest request, CancellationToken cancellationToken)
        {
           await _productService.StockUpdateToProductAsync(request.ProductId, request.Stock);
            return new();
        }
    }
}
