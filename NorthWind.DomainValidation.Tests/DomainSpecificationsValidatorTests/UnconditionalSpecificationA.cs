using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests;

internal class UnconditionalSpecificationA: DomainSpecificationBase<UserRegistration>
{
    public UnconditionalSpecificationA()
    {
        Property(u => u.Email)
            .IsRequired(UserRegistration.IsRequiredErrorMessage);
    }
}
