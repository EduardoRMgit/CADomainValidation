using NorthWind.DomainValidation.Implementations;
using NorthWind.DomainValidation.Interfaces;
using NorthWind.DomainValidation.Tests.Models;
using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.SpecificationTest
{
    public class SpecificationTest
    {
        // pruebos que se decoran con fact, eso es una prueba, ejecutaa porfavor
        // prueba 
        // 3 fases de una prueba, 
        [Fact]
        public void IsSatisfiedBy_ShouldReturnFalse_WhenValidationFails()
        {
            // Arrange
            var Entity = new CreateOrder { CustomerId = null };


            // Act
            var Result = Specification.IsSatisfiedBy(Entity); 

            // Asset
            Assert.False(Result);
            Assert.Single(Specification.Errors);
            Assert.Equal("Property is required", 
                Specification.Errors.First().ErrorMessage);
        }

        [Fact]
        public void IsSatisfiedBy_ShouldReturnFalse_WhenValidationPasses()
        {
            // Arrange
            var Entity = new CreateOrder { CustomerId = "Id1" };


            // Act
            var Result = Specification.IsSatisfiedBy(Entity);

            // Asset
            Assert.True(Result);
            Assert.True(Specification.Errors == null ||
                !Specification.Errors.Any());
        }



        ISpecification<CreateOrder> Specification = new Specification<CreateOrder>(
            entity =>
            {
                IEnumerable<SpecificationError> Errors = [];

                // logica de negocio
                if(string.IsNullOrWhiteSpace(entity.CustomerId))
                {
                    Errors = new List<SpecificationError>()
                    {
                        new SpecificationError("CustomerId", "Property is required")
                    };
                }

                return Errors;
            }
            
            );
    }
}
