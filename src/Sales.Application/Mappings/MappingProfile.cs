using AutoMapper;
using Sales.Application.Features.Sales.Command.UpdateSales;
using Sales.Application.Features.Sales.Queries.GetSalesByRegion;

namespace Sales.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Sales, SalesByRegionDto>();
            CreateMap<UpdateSalesCommand, Domain.Entities.Sales>()
                .IgnoreBaseEntityProperties<UpdateSalesCommand, Domain.Entities.Sales, long>();
        }
    }
}
