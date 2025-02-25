using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests;

public class ConditionalSpecificationC: DomainSpecificationBase<UserRegistration>
{
    public ConditionalSpecificationC() : base(true)
    {

    }

    protected override async Task<List<SpecificationError>> ValidateSpecificationAsync(
        UserRegistration entity)
    {
        List<SpecificationError> Errors = [];
        await Task.Delay(1000);

        if(entity.Password != entity.ConfirmPassword)
        {
            Errors.Add(new SpecificationError(
                "ConfirmPassword",
                UserRegistration.PassWordConfirmationNotMatchErrorMessage));
        }

        return Errors;
    }
}
