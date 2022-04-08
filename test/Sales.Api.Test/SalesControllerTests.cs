using Xunit;
using Sales.Api.Controllers;
using Moq;
using MediatR;
using System.Threading.Tasks;
using Sales.Application.Features.Sales.Queries.GetSalesByRegion;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sales.Test.Common;
using Sales.Application.Contracts.Persistence;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Sales.Application.Features.Sales.Command.UpdateSales;
using Sales.Application.Exceptions;
using System;

namespace Sales.Api.Test
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task GetSalesByRegion_WithAnyRegion_InvokesGetSalesByRegionQuery()
        {
            var mediator = CreateMockedMediator();
            var controller = new SalesController(mediator.Object);

            var result = await controller.GetSalesByRegion(string.Empty);
            mediator.VerifyMediator<GetSalesByRegionQuery, IList<SalesByRegionDto>>(Times.Once(), (request) => true);
        }

        [Theory]
        [InlineData("East")]
        [InlineData("West")]
        public async Task GetSalesByRegion_WithValidRegion_ReturnsNonEmptyCollection(string region)
        {
            var mediator = CreateMockedMediator();
            var controller = new SalesController(mediator.Object);

            var result = await controller.GetSalesByRegion(region);

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsAssignableFrom<List<SalesByRegionDto>>(viewResult.Value);

            Assert.NotEmpty(items);
        }

        [Theory]
        [InlineData("Unknown")]
        [InlineData(null)]
        public async Task GetSalesByRegion_WithInvalidRegion_ReturnsEmptyCollection(string region)
        {
            var mediator = CreateMockedMediator();

            var controller = new SalesController(mediator.Object);

            var result = await controller.GetSalesByRegion(region);

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsAssignableFrom<List<SalesByRegionDto>>(viewResult.Value);

            Assert.Empty(items);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123)]
        public async Task UpdateSalesRecord_WithInvalidId_Throws404Exception(long id)
        {
            var mediator = CreateMockedMediator();
            var controller = new SalesController(mediator.Object);

            var updateCommand = new UpdateSalesCommand() 
            { 
                Id = id
            };

            var exception = await Assert.ThrowsAsync<AggregateException>(() => controller.UpdateSalesRecord(updateCommand));
            Assert.IsType<NotFoundException>(exception.InnerException);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5000)]
        public async Task UpdateSalesRecord_WithValidId_ReturnsNoContent(long id)
        {
            var mediator = CreateMockedMediator();
            var controller = new SalesController(mediator.Object);

            var updateCommand = new UpdateSalesCommand()
            {
                Id = id
            };

            var response = await controller.UpdateSalesRecord(updateCommand);
            Assert.IsType<NoContentResult>(response);
        }

        private static Mock<IMediator> CreateMockedMediator()
        {
            var mediator = new Mock<IMediator>();

            mediator.MockMediator<GetSalesByRegionQuery, IList<SalesByRegionDto>>((request) =>
            {
                if ("East".Equals(request.RegionName))
                {
                    return new List<SalesByRegionDto>
                    {
                        new SalesByRegionDto { Country = "US", ItemType = "Collectibles" }
                    };
                }

                if ("West".Equals(request.RegionName))
                {
                    return new List<SalesByRegionDto>
                    {
                        new SalesByRegionDto { Country = "US", ItemType = "Sporting Goods" }
                    };
                }

                return new List<SalesByRegionDto>();
            });


            mediator.MockMediator<UpdateSalesCommand>((request) => 
            {
                var salesRepository = CreateMockedSalesRepository();
                var mapper = new Mock<IMapper>();
                var logger = new Mock<ILogger<UpdateSalesCommandHandler>>();

                var handler = new UpdateSalesCommandHandler(salesRepository.Object, logger.Object, mapper.Object);
                var result = handler.Handle(request, System.Threading.CancellationToken.None).Result;

                return result;
            });

            return mediator;
        }

        private static Mock<ISalesRepository> CreateMockedSalesRepository()
        {
            var salesRepository = new Mock<ISalesRepository>();

            salesRepository.Setup(x => x.GetByIdAsync(It.IsAny<long>())).Returns((long id) =>
            {
                var response = id <= 0 ? null : new Domain.Entities.Sales { Id = id };
                return Task.FromResult(response);
            });

            return salesRepository;
        }
    }
}