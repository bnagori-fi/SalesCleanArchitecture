using AutoMapper;
using Moq;
using Sales.Application.Contracts.Persistence;
using Sales.Application.Mappings;
using Sales.Application.Features.Sales.Queries.GetSalesByRegion;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using ESales = Sales.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Application.Test
{
    public class GetSalesByRegionHandlerTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("us-east")]
        public async Task GetSalesByRegion_Must_BeVerifiable(string regionName)
        {
            var mockRepository = CreateRepository();
            var mockMapper = new Mock<IMapper>();

            var handler = new GetSalesByRegionQueryHandler(mockRepository.Object, mockMapper.Object);
            var response = await handler.Handle(new GetSalesByRegionQuery(regionName), CancellationToken.None);

            mockRepository.Verify(x => x.GetSalesByRegion(regionName), $"GetSalesByRegion Must Be Called with {regionName}");
            mockMapper.Verify(x => x.Map<IList<SalesByRegionDto>>(It.IsAny<IEnumerable<ESales>>()));
        }

        [Theory]
        [InlineData("us-east")]
        [InlineData("us-north")]
        public async Task GetSalesByRegion_WithValidRegion_NonEmptyCollection(string region)
        {
            var mockRepository = CreateRepository();
            
            var handler = new GetSalesByRegionQueryHandler(mockRepository.Object, CreateAutoMapper());
            var response = await handler.Handle(new GetSalesByRegionQuery(region), CancellationToken.None);

            Assert.NotEmpty(response);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetSalesByRegion_WithInvalidRegion_EmptyCollection(string region)
        {
            var mockRepository = CreateRepository();

            var handler = new GetSalesByRegionQueryHandler(mockRepository.Object, CreateAutoMapper());
            var response = await handler.Handle(new GetSalesByRegionQuery(region), CancellationToken.None);

            Assert.Empty(response);
        }
        private static IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(mc => mc.AddProfile<MappingProfile>());
            return new Mapper(configuration);
        }

        private static Mock<ISalesRepository> CreateRepository()
        {
            var mockRepository = new Mock<ISalesRepository>();

            mockRepository.Setup(x => x.GetSalesByRegion(It.IsAny<string>())).Returns(async (string region) =>
            {
                var sales = string.IsNullOrEmpty(region) 
                    ? Enumerable.Empty<ESales>() 
                    : new[] { new ESales() { Id = 123 } };

                return await Task.FromResult(sales);
            });

            return mockRepository;
        }
    }
}
