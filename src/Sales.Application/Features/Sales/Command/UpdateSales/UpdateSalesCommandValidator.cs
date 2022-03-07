using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Features.Sales.Command.UpdateSales
{
    public class UpdateSalesCommandValidator : AbstractValidator<UpdateSalesCommand>
    {
        public UpdateSalesCommandValidator()
        {
            RuleFor(s => s.OrderID).NotEmpty().WithMessage("{OrderId} is required");
            RuleFor(s => s.UnitsSold).GreaterThan(0).WithMessage("{UnitsSold} should be greater than zero");
        }
    }
}
