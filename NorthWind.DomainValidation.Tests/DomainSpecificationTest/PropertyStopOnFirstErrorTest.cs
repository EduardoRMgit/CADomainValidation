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
    public class PropertyStopOnFirstErrorTest
    // 
    {
        // Probar con stop on first error || stop in first error

        // caracteristica de X Unit, pasandole objetos de prueba, combinación de objetos de prueba para no hacer muchos métodos

        // arreglos de objeto
        // alimentar objetos de prueba a nuestra prueba unitaria, no se va a hacer con inline.

        // cada arreglo, se hará para una prueba. Distinto arreglo de objetos, un arreglo a la ves, crear un método
        // estático que devuelve la colección de un arreglo de objetos, son objetos porque los valores pueden ser de distintos tipos.

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

            // hay errores, revisar que los errores sean los mismos que estámos esperando
            if(Result == false)
            {

                // primero los ordeno los mensajes por propiedad, y luego por msgs.
                var ExpectedErrorsOrdered =
                    expectedErrors.OrderBy(e => e.PropertyName).ThenBy(e => e.ErrorMessage);
                var ActualErrorsOrdered =
                    specification.Errors.OrderBy(e => e.PropertyName).ThenBy(e => e.ErrorMessage);


                // comparar los errores esperados contra el validate async, se ordenan los errores esperados y recibidos
                // recorremos la colección y vamos comparando.

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
                new PropertyWithoutStopInFirstError(),
                new UserRegistration{
                    Password = "Mm1234567$"
                },
                true,
                new List<SpecificationError>()
            };

            yield return new object[]
            {
                new PropertyWithoutStopInFirstError(),
                new UserRegistration{
                    Password = ""
                },
                false,
                new List<SpecificationError>()
                {
                    new SpecificationError("Password", 
                    UserRegistration.HasMinLengthErrorMessage),
                    new SpecificationError("Password",
                    UserRegistration.UpperCaseCharactersAreRequiredErrorMessage),
                    new SpecificationError("Password",
                    UserRegistration.LowerCaseCharactersAreRequiredErrorMessage),
                    new SpecificationError("Password",
                    UserRegistration.DigitAreRequiredErrorMessage),
                    new SpecificationError("Password",
                    UserRegistration.SymbolsAreRequiredErrorMessage)
                }
            };

            yield return new object[]
            {
                new PropertyWithtStopInFirstError(),
                new UserRegistration{
                    Password = "Mm1234567$"
                },
                true,
                new List<SpecificationError>()
            };


            yield return new object[]
            {
                new PropertyWithtStopInFirstError(),
                new UserRegistration{
                    Password = "1234567"
                },
                false,
                new List<SpecificationError>()
                {
                    new SpecificationError("Password",
                    UserRegistration.UpperCaseCharactersAreRequiredErrorMessage)
                }
            };


            yield return new object[]
            {
                new PropertyWithtStopInFirstError(),
                new UserRegistration{
                    Password = "MnabcdEF"
                },
                false,
                new List<SpecificationError>()
                {
                    new SpecificationError("Password",
                    UserRegistration.DigitAreRequiredErrorMessage)
                }
            };


        }



    }
}
