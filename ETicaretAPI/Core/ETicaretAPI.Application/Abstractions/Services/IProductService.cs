namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<byte[]> QRCodeToProductAsync(string id);
        Task StockUpdateToProductAsync(string id, int stock);
    }
}
