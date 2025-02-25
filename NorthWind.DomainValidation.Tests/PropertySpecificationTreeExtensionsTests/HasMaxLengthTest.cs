namespace NorthWind.DomainValidation.Tests.PropertySpecificationTreeExtensionsTests
{
    public class HasMaxLengthTest
    {
        static Expression<Func<CreateOrder, string>> PropertyExpression =
            x => x.CustomerId;

        [Theory]
        [InlineData("1234567", 6, false)]
        [InlineData("", 6, true)]
        [InlineData("123456", 6, true)]
        [InlineData("12345", 6, true)]
        public void HasMaxLength_ShouldReturnExpectedResult_WhenValueIsChecked(
            string value, int length, bool expectedResult)
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            Tree.HasMaxLength(length);
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
        public void HasMaxLength_ShouldUseProvidedErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = "HasMaxLength";
            Tree.HasMaxLength(6, ExpectedErrorMessage);

            var Entity = new CreateOrder { CustomerId = "1234567" };
            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }

        [Fact]
        public void HasMaxLength_ShouldUseDefaultErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<CreateOrder, string>(PropertyExpression);
            string ExpectedErrorMessage = string.Format(ErrorMessages.HasMaxLength, 6);
            Tree.HasMaxLength(6);
            var Entity = new CreateOrder { CustomerId = "1234567" };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);


            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }



    }
}
