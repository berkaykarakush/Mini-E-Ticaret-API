using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            =>new() 
            {
                new(){Name="Product 1", Price=100, Id = Guid.NewGuid()},
                new(){Name="Product 2", Price=200, Id = Guid.NewGuid()},
                new(){Name="Product 3", Price=300, Id = Guid.NewGuid()},
                new(){Name="Product 4", Price=400, Id = Guid.NewGuid()},
                new(){Name="Product 5", Price=500, Id = Guid.NewGuid()}
            };
    }
}
