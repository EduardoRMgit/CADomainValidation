using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.Models;
public class CreateOrderDetail
{
    public const string ProductIdMessage = "Producto requerido.";
    public const string UnitPriceMessage = "Precio requerido.";
    public const string QuantityMessage = "Cantidad requerida.";

    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
}
