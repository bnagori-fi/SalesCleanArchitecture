using AutoMapper;
using Sales.Application.Features.Sales.Command.UpdateSales;
using Sales.Application.Features.Sales.Queries.GetSalesByRegion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Sales, SalesByRegionDto>();
            CreateMap<UpdateSalesCommand, Domain.Entities.Sales>();
        }
    }
}
