using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        public ProductController(
            IProductWriteRepository productWriteRepository, 
            IProductReadRepository productReadRepository,
            ICustomerReadRepository customerReadRepository,
            ICustomerWriteRepository customerWriteRepository,
            IOrderReadRepository orderReadRepository,
            IOrderWriteRepository orderWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }
        [HttpGet]
        public async Task Get ()
        {
            Order order = await _orderReadRepository.GetByIdAsync("21251141-79cd-4142-a624-666a9713420e");
            order.Address = "Istanbul";
            await _orderWriteRepository.SaveAsync();
        }
     
    }
}
