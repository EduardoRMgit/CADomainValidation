using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests;

public class ConditionalSpecificationA: DomainSpecificationBase<UserRegistration>
{
    public ConditionalSpecificationA() : base(true)
    {
        Property(u => u.Email)
            .IsRequired(UserRegistration.IsRequiredErrorMessage);
    }
}
