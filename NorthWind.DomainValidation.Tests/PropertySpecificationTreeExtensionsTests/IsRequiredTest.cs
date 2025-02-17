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
    public class IsRequiredTest
    {
        static Expression<Func<CreateOrder, string>> PropertyExpression =
            x => x.CustomerId;

        [Theory] // le podemos pasar una secuencia de datos que queremos que valide y se lo vamos pasando con inline data
        [InlineData(null, false)]
        [InlineData("    ", false)]
        [InlineData("", false)]
        [InlineData("Usuario1", true)]
        public void IsRequired_ShouldReturnExpectedResult_WhenValueisChecked(
            string customerId, bool expectedResult)
        {

            // Arrange
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            Tree.IsRequired();
            var Entity = new CreateOrder { CustomerId = customerId }; 

            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);


            // Assert
            Assert.Equal(expectedResult, Result);

            if(expectedResult)
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
        public void IsRequired_ShouldUseProvidedErrorMessage_WhenValidationFails()
        {
                        // Arrange
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = "Dato requerido";
            Tree.IsRequired(ExpectedErrorMessage);
            var Entity = new CreateOrder { CustomerId = null }; 

            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);


            // Assert
            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }


        [Fact]
        public void IsRequired_ShouldUseDefaultErrorMessage_WhenValidationFails()
        {
            // Arrange
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = ErrorMessages.IsRequired;
            Tree.IsRequired(ExpectedErrorMessage);
            var Entity = new CreateOrder { CustomerId = null };

            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);


            // Assert
            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }
    }
}
