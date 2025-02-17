using NorthWind.DomainValidation.Implementations;
using NorthWind.DomainValidation.PropertySpecificationTreeExtensions;
using NorthWind.DomainValidation.Resources;
using NorthWind.DomainValidation.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace NorthWind.DomainValidation.Tests.PropertySpecificationTreeExtensionsTests
{
    public class HasFixedLengthTests
    {

        static Expression<Func<CreateOrder, string>> PropertyExpression =
            x => x.CustomerId;

        [Theory] // le podemos pasar una secuencia de datos que queremos que valide y se lo vamos pasando con inline data
        [InlineData(null, 5, false)]
        [InlineData("1234567", 5, false)]
        [InlineData("", 5, false)]
        [InlineData("1234567", 7, true)]
        public void HasFixedLength_ShouldReturnExpectedResult_WhenValueIsChecked(
            string value, int length, bool expectedResult)
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            Tree.HasFixedLength(length);
            var Entity = new CreateOrder { CustomerId = value };


            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);


            // Assert
            Assert.Equal(expectedResult, Result);

            if (expectedResult)
            {
                Assert.True(Tree.Specifications[0].Errors == null ||
                    !Tree.Specifications[0].Errors.Any());

            }
            else
            {
                Assert.Single(Tree.Specifications[0].Errors);
            }
        }


        [Fact]
        public void HasFixedLength_ShouldUseProvidedErrorMessage_WhenValidationFails()
        {
            // Arrange
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = "HasFixedLength";
            Tree.HasFixedLength(5, ExpectedErrorMessage);

            var Entity = new CreateOrder { CustomerId = "123" };

            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            // Assert
            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }

        [Fact]
        public void HasFixed_Length_ShouldUseDefaultErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = string.Format(ErrorMessages.HasFixedLength, 5);

            Tree.HasFixedLength(5);
            var Entity = new CreateOrder { CustomerId = "1234567" };

            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            // Assert
            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }


    }
}
