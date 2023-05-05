using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretAPIDbContext>
    {
        //IoC container'a koyucagimiz ctor CMD uzerinden migration basabilmemiz icin DesignTimeFactory nesnesini olusturmamiz gerekiyor ki hata almadan dotnet cli uzerinden migration islemlerimizi gerceklestirebilelim. Appsettings.json dosyasindan harici yapilandirma yapma islemi icin ConfigurationManager sinifini kullaniyoruz. Bu sinif .Net6 ile birlikte gelmistir. Bu sinif icin Microsoft.Extensions.Cnfiguration paketini yuklemeleyiz. Ayrica baska bir katmandaki appsettings.json dosyasina ulasmak icin AddJsonFile() metotunu kullanabilmemiz icin ise Microsoft.Extensions.Configuration.Json kutuphanesini de projeye eklemeliyiz. Ayrica appsettings.json dosyasinin bulundugu dizinin yolunu da belirtmemiz gerekmekyedir bu islemi ise SetBasePath() metotu araciligiyla gerceklestirebilmekteyiz.
        public ETicaretAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
