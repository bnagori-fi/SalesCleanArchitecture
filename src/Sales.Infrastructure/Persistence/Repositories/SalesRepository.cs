using Sales.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Persistence.Repositories
{
    public class SalesRepository : RepositoryBase<Domain.Entities.Sales, long>, ISalesRepository
    {
        public SalesRepository(SalesDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Domain.Entities.Sales>> GetSalesByRegion(string regionName)
        {
            var salesByRegionList = await GetAsync(s => s.Region == regionName);
            return salesByRegionList;
        }

        public async Task<IEnumerable<Domain.Entities.Sales>> GetSalesGreaterThanUnitsSold(int unitsSold)
        {
            var salesGreaterThanUnitsSold = await GetAsync(s => s.UnitsSold >= unitsSold);
            return salesGreaterThanUnitsSold;
        }
    }
}
