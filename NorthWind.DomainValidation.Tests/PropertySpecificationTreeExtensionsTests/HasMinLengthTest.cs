namespace NorthWind.DomainValidation.Tests.PropertySpecificationTreeExtensionsTests
{
    public class HasMinLengthTest
    {
        static Expression<Func<CreateOrder, string>> PropertyExpression =
            x => x.CustomerId;

        [Theory]
        [InlineData("1234", 5, false)]
        [InlineData("", 5, false)]
        [InlineData("12345", 5, true)]
        [InlineData("123456", 5, true)]
        public void HasMinLength_ShouldReturnExpectedResult_WhenValueIsChecked(
            string value, int length, bool expectedResult)
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            Tree.HasMinLength(length);
            var Entity = new CreateOrder { CustomerId = value };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

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
        public void HasMinLength_ShouldUseProvidedErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = "HasMinLength";
            Tree.HasMinLength(5, ExpectedErrorMessage);

            var Entity = new CreateOrder { CustomerId = "1234" };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }

        [Fact]
        public void HasMixLength_ShouldUseDefaultErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = string.Format(ErrorMessages.HasMinLength, 5);
            Tree.HasMinLength(5);
            var Entity = new CreateOrder { CustomerId = "1234" };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);


            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }

    }
}
