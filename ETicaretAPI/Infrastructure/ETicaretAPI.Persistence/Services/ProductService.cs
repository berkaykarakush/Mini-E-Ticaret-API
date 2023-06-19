using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using System.Text.Json;

namespace ETicaretAPI.Persistence.Services
{
    public class ProductService : IProductService
    {
        readonly IQRCodeService _qrCodeService;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        public ProductService(IQRCodeService qrCodeService, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _qrCodeService = qrCodeService;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<byte[]> QRCodeToProductAsync(string id)
        {
           Product product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            var plaintObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreatedDate
            };

            string plainText = JsonSerializer.Serialize(plaintObject);
            return _qrCodeService.GenerateQRCode(plainText);
        }

        public async Task StockUpdateToProductAsync(string id, int stock)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            product.Stock = stock;
            await _productWriteRepository.SaveAsync();
        }
    }
}
