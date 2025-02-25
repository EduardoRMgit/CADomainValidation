using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests;

internal class UnconditionalSpecificationC: DomainSpecificationBase<UserRegistration>
{
    public UnconditionalSpecificationC()
    {
        Property(u => u.ConfirmPassword)
            .Equal(u => u.Password, UserRegistration.PassWordConfirmationNotMatchErrorMessage);
    }

}
