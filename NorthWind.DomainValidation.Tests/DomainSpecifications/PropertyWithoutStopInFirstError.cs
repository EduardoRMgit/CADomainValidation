using NorthWind.DomainValidation.Implementations;
using NorthWind.DomainValidation.PropertySpecificationTreeExtensions;
using NorthWind.DomainValidation.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecifications
{
    internal class PropertyWithoutStopInFirstError : DomainSpecificationBase<CreateOrder>
    {
        public PropertyWithoutStopInFirstError()
        {
            Property(o => o.CustomerId)
                .IsRequired()
                .HasFixedLength(5);
        }

    }
}
