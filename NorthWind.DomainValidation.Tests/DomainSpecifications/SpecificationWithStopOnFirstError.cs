using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecifications
{
    internal class SpecificationWithStopOnFirstError: DomainSpecificationBase<UserRegistration>
    {

        public SpecificationWithStopOnFirstError()
        {
            StopOnFirstError = true;

            // aquí tenemos nuestra especificación
            Property(e => e.Email)
                .IsRequired(UserRegistration.IsRequiredErrorMessage);

            Property(u => u.Password)
                .HasMinLength(6, UserRegistration.HasMinLengthErrorMessage);

            Property(u => u.ConfirmPassword)
                .Equal(u => u.Password, UserRegistration.PassWordConfirmationNotMatchErrorMessage);

        }

    }
}
