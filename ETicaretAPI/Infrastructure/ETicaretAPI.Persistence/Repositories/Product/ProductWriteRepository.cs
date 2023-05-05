using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    //Sadece IProductWriteRepository'den inherit almis olsaydik her metodu ayri ayri doldurmamiz gerekecekti, e biz zaten bu metotlari doldurduk bunun icin writerepository<T> seklinde metotlarin doldurulmus halini dahil edebiliyoruz. IProductWriteRepository'i ekleme sebebimiz ise bu concrete nesnesinin abstractionidir yan dependency injectiondan talep ederken IProductWriteRepository'i kullaniyoruz. Eger ctor icerisinde islem yapilmazsa metot hata verecektir
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
