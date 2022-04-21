using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Sales.Application.Contracts.Persistence;
using Sales.Application.Exceptions;
using Sales.Application.Features.Sales.Command.UpdateSales;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sales.Application.Test
{
    public class UpdateSalesCommandTests
    {
        [Fact]
        public async Task UpdateSalesCommand_IdNotFound_ThrowsNotFoundException()
        {
            var mockRepository = new Mock<ISalesRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<UpdateSalesCommandHandler>>();
            
            var handler = new UpdateSalesCommandHandler(mockRepository.Object, mockLogger.Object, mockMapper.Object);
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new UpdateSalesCommand(), CancellationToken.None));
            mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Theory]
        [InlineData(123)]
        [InlineData(321)]
        public async Task UpdateSalesCommand_IdIsFound_ReturnValidResponse(long salesId)
        {
            var mockRepository = CreateRepository(salesId);
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<UpdateSalesCommandHandler>>();

            var handler = new UpdateSalesCommandHandler(mockRepository.Object, mockLogger.Object, mockMapper.Object);
            var response = await handler.Handle(new UpdateSalesCommand() { Id = salesId }, CancellationToken.None);
            
            mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<long>()), Times.Once);
            mockRepository.Verify(x => x.UpdateAsync(It.Is<Domain.Entities.Sales>(s => s.Id == salesId)), Times.Once);
        }

        private static Mock<ISalesRepository> CreateRepository(long salesId)
        {
            var mockRepository = new Mock<ISalesRepository>();

            mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<long>())).Returns(async (long id) =>
            {
                return await Task.FromResult(new Domain.Entities.Sales()
                {
                    Id = id
                });
            });

            return mockRepository;
        }
    }
}
