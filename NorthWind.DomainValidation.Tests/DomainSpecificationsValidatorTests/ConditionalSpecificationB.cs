using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests;

public class ConditionalSpecificationB: DomainSpecificationBase<UserRegistration>
{
    public ConditionalSpecificationB() : base(true)
    {
        Property(u => u.Password)
            .HasMinLength(6, UserRegistration.HasMinLengthErrorMessage);
    }
}
