using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests;

internal class UnconditionalSpecificationB: DomainSpecificationBase<UserRegistration>
{
    public UnconditionalSpecificationB()
    {
        Property(u => u.Password)
            .HasMinLength(6, UserRegistration.HasMinLengthErrorMessage);
    }
}
