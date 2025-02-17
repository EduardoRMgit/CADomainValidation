using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Extensions
{
    // extensions y helpers => staticas

    // el scope ya va dependiendo
    internal static class ExpressionsExtensions
    {
        // método genérico
        public static string GetPropertyName<T, TProperty>(this Expression<Func<T , TProperty>> propertyExpression)
        {
            string PropertyName = null;

            // las expresiones tienen un cuerpo
            // puede ser una buena expresión o una expresión unaria, 

            // si el compilador me puede devolver
            var Body = propertyExpression.Body;

            if(Body is UnaryExpression UnaryExpression)
            {
                // propiedad de tipo entero, transforma en una conversión, cuando le pasamos 
                // d => d.ProductId =>  el compilador de LINQ lo transforma en esta expresión: d >= Convert(d.ProductId, object) , es una expresión, unaria a un solo operando.
                Body = UnaryExpression.Operand;
            }

            if (Body is MemberExpression MemberExpression)
            {
                PropertyName = MemberExpression.Member.Name;
            }


            return PropertyName;
        }


    }
}
