using NorthWind.DomainValidation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.SetValidatorTests;

internal class CreateOrderSpecification : DomainSpecificationBase<CreateOrder>
{
    // se va a encargar de validar la colección
    public CreateOrderSpecification(IDomainSpecificationValidator<CreateOrderDetail> 
        orderDetailsValidator)
    {
        Property(o => o.CustomerId)
            .IsRequired();

        Property(o => o.OrderDetails)
            .NotEmpty();

        Property(o => o.OrderDetails)
            .SetValidator(orderDetailsValidator);
        // va a validar dependiendo de la orderdetailsValidator

    }
}
