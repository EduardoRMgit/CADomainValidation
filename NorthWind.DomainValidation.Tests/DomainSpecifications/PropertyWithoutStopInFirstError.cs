﻿namespace NorthWind.DomainValidation.Tests.DomainSpecifications
{

    // LA ESPECIFICATION HEREDA DE DOMAIN SPECIFICATION BASE.

    // sería equivalente a lo de fluentValidation.

    // Con AbstractValiator => DomainSpecificationBase<CreateOrder>
    internal class PropertyWithtStopInFirstError : DomainSpecificationBase<UserRegistration>
    {
        public PropertyWithtStopInFirstError()
        {
            Property(u => u.Password, true)
                .HasMinLength(6, UserRegistration.HasMinLengthErrorMessage)
                .Must(p => p.Any(c => char.IsUpper(c)),
                UserRegistration.UpperCaseCharactersAreRequiredErrorMessage)
                // ANY = cualquiera
                .Must(p => p.Any(c => char.IsLower(c)),
                UserRegistration.LowerCaseCharactersAreRequiredErrorMessage)
                .Must(p => p.Any(c => char.IsDigit(c)),
                UserRegistration.DigitAreRequiredErrorMessage)
                .Must(p => p.Any(c => !char.IsLetterOrDigit(c)),
                UserRegistration.SymbolsAreRequiredErrorMessage);
        }

    }
}
