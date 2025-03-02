using NorthWind.DomainValidation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.SetValidatorTests
{

    public class SetValidatorTest
    {
        [Theory] // el member data, los datos
        [MemberData(nameof(SetValidatorTestData.GetTestData),
        MemberType = typeof(SetValidatorTestData))]

        public async Task
            ValidateAsyn_ShouldReturnExpectedResult_WhenValidateDomainSpecification(
            CreateOrder order,
            ValidationResult expectedResult)
        {
            // Arrange

            // validador de Create OrderDetail
            IDomainSpecificationValidator<CreateOrderDetail> OrderDetailValidator =
                new DomainSpecificationValidator<CreateOrderDetail>(
                    [new CreateOrderDetailSpecification()]);

            // Validador de Create Order
            IDomainSpecificationValidator<CreateOrder> CreateOrderValidator =
                new DomainSpecificationValidator<CreateOrder>(
                    [new CreateOrderSpecification(OrderDetailValidator)]);

            // Act
            var Result = await CreateOrderValidator.ValidateAsync(order);


            // Assert
            Assert.Equal(expectedResult.IsValid, Result.IsValid);

            if(Result.IsValid == false)
            {
                // Verificar que vengan los mensajes de 
                var ExpectedErrorsOrdered =
                    expectedResult.Errors.OrderBy(e => e.PropertyName)
                    .ThenBy(e => e.ErrorMessage);

                // Verificar que vengan los mensajes de 
                var ActualErrorsOrdered =
                    Result.Errors.OrderBy(e => e.PropertyName)
                    .ThenBy(e => e.ErrorMessage);

                Assert.Collection(
                    ActualErrorsOrdered,
                    ExpectedErrorsOrdered
                    .Select(expected => (Action<SpecificationError>)(actual =>
                    {
                        Assert.Equal(expected.PropertyName, actual.PropertyName);
                        Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
                    })).ToArray());
            }
            else
            {
                Assert.True(Result.Errors == null || !Result.Errors.Any());
            }

        }
    }
}
