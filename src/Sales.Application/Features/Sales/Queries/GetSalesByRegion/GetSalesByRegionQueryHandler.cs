using AutoMapper;
using MediatR;
using Sales.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Features.Sales.Queries.GetSalesByRegion
{
    public class GetSalesByRegionQueryHandler : IRequestHandler<GetSalesByRegionQuery, IList<SalesByRegionDto>>
    {
        private readonly ISalesRepository salesRepository;
        private readonly IMapper mapper;

        public GetSalesByRegionQueryHandler(ISalesRepository salesRepository, IMapper mapper)
        {
            this.salesRepository = salesRepository;
            this.mapper = mapper;
        }

        public async Task<IList<SalesByRegionDto>> Handle(GetSalesByRegionQuery request, CancellationToken cancellationToken)
        {
            var salesByRegionList = await salesRepository.GetSalesByRegion(request.RegionName);
            var salesByRegionDtoList = mapper.Map<IList<SalesByRegionDto>>(salesByRegionList);
            return salesByRegionDtoList;
        }
    }
}
