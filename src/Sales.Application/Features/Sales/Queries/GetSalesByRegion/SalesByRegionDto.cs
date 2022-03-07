using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Features.Sales.Queries.GetSalesByRegion
{
    public class SalesByRegionDto
    {
        public string Country { get; set; }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public int UnitsSold { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
