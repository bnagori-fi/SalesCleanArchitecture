using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sales.Application.Contracts.Persistence;
using Sales.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Features.Sales.Command.UpdateSales
{
    public class UpdateSalesCommandHandler : IRequestHandler<UpdateSalesCommand>
    {
        private readonly ISalesRepository salesRepository;
        private readonly ILogger<UpdateSalesCommandHandler> logger;
        private readonly IMapper mapper;

        public UpdateSalesCommandHandler(ISalesRepository salesRepository, ILogger<UpdateSalesCommandHandler> logger, IMapper mapper)
        {
            this.salesRepository = salesRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSalesCommand request, CancellationToken cancellationToken)
        {
            var salesRecordToUpdate = await salesRepository.GetByIdAsync(request.Id);
            if (salesRecordToUpdate == null)
            {
                logger.LogError("Sales record does not exist in database. Sales Id: {Id}", request.Id);
                throw new NotFoundException(nameof(Domain.Entities.Sales), request.Id);
            }

            mapper.Map(request, salesRecordToUpdate);

            await salesRepository.UpdateAsync(salesRecordToUpdate);
            logger.LogInformation("Sales record updated successfully {Id}", request.Id);

            return Unit.Value;
        }
    }
}
