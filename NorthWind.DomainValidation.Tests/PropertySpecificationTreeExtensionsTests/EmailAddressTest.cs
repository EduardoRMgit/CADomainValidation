

namespace NorthWind.DomainValidation.Tests.PropertySpecificationTreeExtensionsTests
{
    public class EmailAddressTest
    {
        static Expression<Func<User, string>> PropertyExpression =
            x => x.EmailAddress;

        [Theory]
        [InlineData("prueba@example", false)]
        [InlineData("prueba@.com", false)]
        [InlineData("prueba@example..com", false)]
        [InlineData("prueba @example.com", false)]
        [InlineData("prueba#@example.com", false)]
        [InlineData(".prueba#@example.com", false)]
        [InlineData("prueba@example.com", true)]
        [InlineData("prueba@example.es", true)]

        public void EmailAddress_ShouldReturnExpectedResult_WhenValueIsChecked(
            string value, bool expectedResult)
        {
            var Tree = new PropertySpecificationsTree<User, string>(PropertyExpression);
            Tree.EmailAdressValidation();
            var Entity = new User { EmailAddress = value };

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
        public void EmailAddress_ShouldUseProvidedErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<User, string>(PropertyExpression);
            string ExpectedErrorMessage = "EmailAddressValidation";
            Tree.EmailAdressValidation(ExpectedErrorMessage);

            var Entity = new User { EmailAddress = "prueba@example" };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }


        [Fact]
        public void EmailAddress_ShouldUseDefaultErrorMessage_WhenValidationFails()
        {
            var Tree = new PropertySpecificationsTree<User, string>(PropertyExpression);
            string ExpectedErrorMessage = string.Format(ErrorMessages.EmailAddress, 5);

            Tree.EmailAdressValidation();
            var Entity = new User { EmailAddress = "prueba@example" };

            bool Result = Tree.Specifications[0].IsSatisfiedBy(Entity);

            Assert.False(Result);
            Assert.Equal(ExpectedErrorMessage, Tree.Specifications[0].Errors.First().ErrorMessage);
        }



    }
}
