using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.SetValidatorTests
{
    public static class SetValidatorTestData
    {
        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
                new CreateOrder
                {
                    CustomerId = "Alfki",
                    OrderDetails = [
                        new CreateOrderDetail
                        {
                            ProductId = 1,
                            Quantity = 2,
                            UnitPrice = 3
                        }
                    ]
                },
                new ValidationResult([])
            };
            // esperamos un new validation result sin errores

            yield return new object[]
            {
                new CreateOrder
                {
                    CustomerId = "Alfki",
                    OrderDetails = null
                },
                new ValidationResult([
                    new SpecificationError("OrderDetails", ErrorMessages.NotEmpty)])
            };

            yield return new object[]
            {
                new CreateOrder
                {
                    CustomerId = "Alfki",
                    OrderDetails = [

                        ]
                },
                new ValidationResult([
                    new SpecificationError("OrderDetails", ErrorMessages.NotEmpty)])
            };

            yield return new object[]
            {
                new CreateOrder
                {
                    CustomerId = "Alfki",
                    OrderDetails = [
                        new CreateOrderDetail{
                            ProductId = 1,
                            Quantity = 2,
                            UnitPrice = 3
                        },
                        new CreateOrderDetail{
                            ProductId = 0,
                            Quantity = 2,
                            UnitPrice = 3
                        },
                        new CreateOrderDetail{
                            ProductId = 1,
                            Quantity = 0,
                            UnitPrice = 3
                        },
                        new CreateOrderDetail{
                            ProductId = 1,
                            Quantity = 2,
                            UnitPrice = 0
                        },
                        ]
                },
                new ValidationResult([
                    new SpecificationError("OrderDetails[1].ProductId", CreateOrderDetail.ProductIdMessage),
                    new SpecificationError("OrderDetails[2].Quantity", CreateOrderDetail.QuantityMessage),
                    new SpecificationError("OrderDetails[3].UnitPrice", CreateOrderDetail.UnitPriceMessage),
                ])
            };

        }
    }
}
