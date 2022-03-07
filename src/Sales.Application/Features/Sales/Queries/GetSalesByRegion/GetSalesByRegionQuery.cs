using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Features.Sales.Queries.GetSalesByRegion
{
    public class GetSalesByRegionQuery : IRequest<IList<SalesByRegionDto>>
    {
        public string RegionName { get; }

        public GetSalesByRegionQuery(string regionName)
        {
            RegionName = regionName;
        }
    }
}
