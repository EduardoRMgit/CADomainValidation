using NorthWind.DomainValidation.Interfaces;
using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests
{
    public class TestData
    {

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {new UnconditionalSpecificationA()},
                new UserRegistration
                {
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "Mn1234567$"
                },
                new ValidationResult([])
            };

            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {new UnconditionalSpecificationA()},
                new UserRegistration
                {
                    Email = "",
                    Password = "Mn1234567$",
                    ConfirmPassword = "Mn1234567$"
                },
                new ValidationResult([
                    new SpecificationError("Email", UserRegistration.IsRequiredErrorMessage)
                    ])
            };


            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new UnconditionalSpecificationA(),
                    new UnconditionalSpecificationB(),
                    new UnconditionalSpecificationC()},
                new UserRegistration
                {
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "Mn1234567$"
                },
                new ValidationResult([])
            };


            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new UnconditionalSpecificationA(),
                    new UnconditionalSpecificationB(),
                    new UnconditionalSpecificationC()},
                new UserRegistration
                {
                    Email = null,
                    Password = "123",
                    ConfirmPassword = "453"
                },
                new ValidationResult([
                    new SpecificationError("Email", UserRegistration.IsRequiredErrorMessage),
                    new SpecificationError("Password", UserRegistration.HasMinLengthErrorMessage),
                    new SpecificationError("ConfirmPassword", UserRegistration.PassWordConfirmationNotMatchErrorMessage)
                    ])
            };


            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new ConditionalSpecificationA(),
                    new ConditionalSpecificationB(),
                    new ConditionalSpecificationC()},
                new UserRegistration
                {
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "Mn1234567$"
                },
                new ValidationResult([])
            };


            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new ConditionalSpecificationA(),
                    new ConditionalSpecificationB(),
                    new ConditionalSpecificationC()},
                new UserRegistration
                {
                    Email = null,
                    Password = "123",
                    ConfirmPassword = "456"
                },
                new ValidationResult([
                    new SpecificationError("Email", UserRegistration.IsRequiredErrorMessage)
                    ])
            };




            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new UnconditionalSpecificationA(),
                    new ConditionalSpecificationB(),
                    new ConditionalSpecificationC()},
                new UserRegistration
                {
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "Mn1234567$"
                },
                new ValidationResult([])
            };



            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new UnconditionalSpecificationA(),
                    new ConditionalSpecificationB(),
                    new ConditionalSpecificationC()},
                new UserRegistration
                {
                    Email = "",
                    Password = "123",
                    ConfirmPassword = "456"
                },
                new ValidationResult([
                    new SpecificationError("Email", UserRegistration.IsRequiredErrorMessage)])
            };


            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new UnconditionalSpecificationA(),
                    new ConditionalSpecificationB(),
                    new ConditionalSpecificationC()},
                new UserRegistration
                {
                    Email = "name@hotmail.com",
                    Password = "123",
                    ConfirmPassword = "456"
                },
                new ValidationResult([
                    new SpecificationError("Password", UserRegistration.HasMinLengthErrorMessage)])
            };


            yield return new object[]
            {
                new IDomainSpecification<UserRegistration>[]
                {
                    new UnconditionalSpecificationA(),
                    new ConditionalSpecificationB(),
                    new ConditionalSpecificationC()},
                new UserRegistration
                {
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "456"
                },
                new ValidationResult([
                    new SpecificationError("ConfirmPassword", UserRegistration.PassWordConfirmationNotMatchErrorMessage)])
            };

        }

    }
}
