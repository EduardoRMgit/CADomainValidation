namespace NorthWind.DomainValidation.Tests.PropertySpecificationTreeExtensionsTests
{
    public class GreaterThanOrEqualTest
    {


        static Expression<Func<PizzaSpecial, double>> PropertyExpression =
            x => x.BasePrice;

        [Theory] // le podemos pasar una secuencia de datos que queremos que valide y se lo vamos pasando con inline data
        [InlineData(0, 5, false)]
        [InlineData(5, 5, true)]
        [InlineData(5.0, 5, true)]
        [InlineData(6, 5, true)]
        [InlineData(6.0, 5, true)]

        public void GreaterThanOrEqual_ShouldReturnExpectedResult_WhenValueIsChecked(
            double value, double threshold, bool expectedResult)
        {
            var Tree = new PropertySpecificationsTree<PizzaSpecial, double>(PropertyExpression);
            Tree.GreaterThanOrEqual(threshold);
            var Entity = new PizzaSpecial { BasePrice = value };


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
        public void GreaterThanOrEqual_ShouldUseProvidedErrorMessage_WhenValidationFails()
        {
            // Arrange
            var Tree = new PropertySpecificationsTree<PizzaSpecial, double>(PropertyExpression);
            string ExpectedErrorMessage = "HasGreaterThanOrEqual";
            Tree.GreaterThan(5, ExpectedErrorMessage);

            var Entity = new PizzaSpecial { BasePrice = 4 };

            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            // Assert
            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }

        [Fact]
        public void GreaterThanOrEqual_ShouldUseDefaultErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<PizzaSpecial, double>(PropertyExpression);
            string ExpectedErrorMessage = string.Format(ErrorMessages.GreaterThanOrEqual, 5);

            Tree.GreaterThanOrEqual(5);
            var Entity = new PizzaSpecial { BasePrice = 4 };

            // Act
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            // Assert
            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }




    }
}
