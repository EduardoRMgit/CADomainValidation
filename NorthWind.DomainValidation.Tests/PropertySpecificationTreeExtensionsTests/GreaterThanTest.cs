namespace NorthWind.DomainValidation.Tests.PropertySpecificationTreeExtensionsTests
{
    public class GreaterThanTest
    {

        static Expression<Func<PizzaSpecial, double>> PropertyExpression =
            x => x.BasePrice;

        [Theory]
        [InlineData(0, 5, false)]
        [InlineData(5, 5, false)]
        [InlineData(5.0, 5, false)]
        [InlineData(6, 5, true)]
        public void GreaterThan_ShouldReturnExpectedResult_WhenValueIsChecked(
            double value, double threshold, bool expectedResult)
        {
            var Tree = new PropertySpecificationsTree<PizzaSpecial, double>(PropertyExpression);
            Tree.GreaterThan(threshold);
            var Entity = new PizzaSpecial { BasePrice = value };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

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
        public void GreaterThan_ShouldUseProvidedErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<PizzaSpecial, double>(PropertyExpression);
            string ExpectedErrorMessage = "HasGreaterThan";
            Tree.GreaterThan(5, ExpectedErrorMessage);

            var Entity = new PizzaSpecial { BasePrice = 5 };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }


        [Fact]
        public void GreaterThan_ShouldUseDefaultErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<PizzaSpecial, double>(PropertyExpression);
            string ExpectedErrorMessage = string.Format(ErrorMessages.GreaterThan, 5);

            Tree.GreaterThan(5);
            var Entity = new PizzaSpecial { BasePrice = 5 };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }

    }
}
