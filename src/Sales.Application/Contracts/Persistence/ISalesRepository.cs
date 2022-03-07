
namespace Sales.Application.Contracts.Persistence
{
    public interface ISalesRepository : IAsyncRepository<Domain.Entities.Sales, long>
    {
        Task<IEnumerable<Domain.Entities.Sales>> GetSalesByRegion(string regionName);
        Task<IEnumerable<Domain.Entities.Sales>> GetSalesGreaterThanUnitsSold(int unitsSold);


    }
}
