namespace NorthWind.DomainValidation.Tests.Models
{
    public class CreateOrder
    {
        public string CustomerId { get; set; }

        public IEnumerable<CreateOrderDetail> OrderDetails { get; set; }


    }
}
