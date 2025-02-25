using NorthWind.DomainValidation.Interfaces;
using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWind.DomainValidation.ValueObjects;

namespace NorthWind.DomainValidation.Tests.DomainSpecificationsValidatorTests;

public class DomainSpecificationsValidatorTests
{
    // debe retornar el valor esperado
    // De ese elemento, el tipo de miembro, es el tipo TestData, tome los archivos de la clase TestData, sobre el método TestData
    [Theory]
    [MemberData(nameof(TestData.GetTestData),
        MemberType = typeof(TestData))]

    public async Task TaskAsync_ShouldReturnExpectedResult_WhenValidateDomainSpecifications(
        IDomainSpecification<UserRegistration>[] specifications,
        UserRegistration user,
        ValueObjects.ValidationResult expectedResult)
    {
        // Arrange 
        IDomainSpecificationValidator<UserRegistration> Validator =
            new DomainSpecificationValidator<UserRegistration>(specifications);

        // Act
        var Result = await Validator.ValidateAsync(user);

        // Assert
        Assert.Equal(expectedResult.IsValid, Result.IsValid);

        if (!Result.IsValid)
        {
            var ExpectedErrorsOrdered =
                expectedResult.Errors.OrderBy(e => e.PropertyName).ThenBy(e => e.ErrorMessage);

            var ActualErrorsOrdered =
                Result.Errors.OrderBy(e => e.PropertyName).ThenBy(e => e.ErrorMessage);

            Assert.Collection(
                ActualErrorsOrdered,
                ExpectedErrorsOrdered.Select(expected =>
                    new Action<SpecificationError>(actual =>
                    {
                        Assert.Equal(expected.PropertyName, actual.PropertyName);
                        Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
                    })
                ).ToArray()
            );
        }
        else
        {
            Assert.True(Result.Errors == null || !Result.Errors.Any());
        }
    }
}