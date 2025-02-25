using NorthWind.DomainValidation.Interfaces;
using NorthWind.DomainValidation.Tests.DomainSpecifications;
using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationTest
{
    public class SpecificationStopOnFirstErrorTest
    // 
    {


        [Theory]
        [MemberData(nameof(GetTestData))]
        public async Task ValidateAsync_ShouldReturnExpectedResult_WhenValidate(
            IDomainSpecification<UserRegistration> specification, UserRegistration entity, bool expectedResult,
            IEnumerable<SpecificationError> expectedErrors)
        {
            // Arrange => preparar mis datos.


            // Act
            var Result = await specification.ValidateAsync(entity);

            // Assert
            Assert.Equal(expectedResult, Result);

            if(Result == false)
            {

                var ExpectedErrorsOrdered =
                    expectedErrors.OrderBy(e => e.PropertyName).ThenBy(e => e.ErrorMessage);
                var ActualErrorsOrdered =
                    specification.Errors.OrderBy(e => e.PropertyName).ThenBy(e => e.ErrorMessage);

                Assert.Collection(
                    ActualErrorsOrdered,
                    ExpectedErrorsOrdered
                    .Select(expected => (Action<SpecificationError>)(actual =>
                    {
                        Assert.Equal(expected.PropertyName, actual.PropertyName);
                        Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
                    })
                    ).ToArray());
            }
            else
            {
                // QUE SEA NULO O QUE NO TENGA NINGÚN ELEMENTO
                Assert.True(specification.Errors == null ||
                    !specification.Errors.Any());
            }
        }


        public static IEnumerable<object[]> GetTestData()
        {

            // CADA UUNO DE LOS ELEMENTOS DE LA COLECCIÓN SE VAYAN DEVOLVIENDO CONFORME SE VAYAN SOLICITANDO
            yield return new object[]
            {
                new SpecificationWithoutStopOnFirstError(),
                new UserRegistration{
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "Mn1234567$",
                },
                true,
                new List<SpecificationError>()
            };

            yield return new object[]
            {
                new SpecificationWithoutStopOnFirstError(),
                new UserRegistration{
                    Email = "",
                    Password = "123",
                    ConfirmPassword = "456",
                },
                false,
                new List<SpecificationError>()
                {
                    new SpecificationError("Email", 
                    UserRegistration.IsRequiredErrorMessage),
                    new SpecificationError("Password",
                    UserRegistration.HasMinLengthErrorMessage),
                    new SpecificationError("ConfirmPassword",
                    UserRegistration.PassWordConfirmationNotMatchErrorMessage)
                }
            };

            yield return new object[]
            {
                new SpecificationWithStopOnFirstError(),
                new UserRegistration{
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "Mn1234567$",
                },
                true,
                new List<SpecificationError>()
            };

            yield return new object[]
            {
                new SpecificationWithStopOnFirstError(),
                new UserRegistration{
                    Email = "",
                    Password = "123",
                    ConfirmPassword = "345",
                },
                false,
                new List<SpecificationError>()
                {
                    new SpecificationError("Email",
                    UserRegistration.IsRequiredErrorMessage)
                }
            };

            yield return new object[]
            {
                new SpecificationWithStopOnFirstError(),
                new UserRegistration{
                    Email = "name@hotmail.com",
                    Password = "123",
                    ConfirmPassword = "345",
                },
                false,
                new List<SpecificationError>()
                {
                    new SpecificationError("Password",
                    UserRegistration.HasMinLengthErrorMessage)
                }
            };



            yield return new object[]
            {
                new SpecificationWithStopOnFirstError(),
                new UserRegistration{
                    Email = "name@hotmail.com",
                    Password = "Mn1234567$",
                    ConfirmPassword = "345",
                },
                false,
                new List<SpecificationError>()
                {
                    new SpecificationError("ConfirmPassword",
                    UserRegistration.PassWordConfirmationNotMatchErrorMessage)
                }
            };


        }



    }
}
