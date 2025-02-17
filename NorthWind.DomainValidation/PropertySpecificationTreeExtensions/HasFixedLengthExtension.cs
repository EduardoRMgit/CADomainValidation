using NorthWind.DomainValidation.Helpers;
using NorthWind.DomainValidation.Implementations;
using NorthWind.DomainValidation.Resources;
using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static class HasFixedLengthExtension
    {
        // (o => o.Customer.ID).IsRequired("es requerido").HasMaxLength("encadenaciones").
        public static PropertySpecificationsTree<T, string> HasFixedLength<T>(
            this PropertySpecificationsTree<T, string> tree,
            int length,
            string errorMessage = default)
        {
            // Agregar una nueva especificación a la lista de especificaciones del árbol
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {
                // Validar si es nulo o string vacío
                if (value == null || (value.Length != length))
                {
                    errors.Add(new SpecificationError(tree.PropertyName,
                        errorMessage ?? string.Format(ErrorMessages.HasFixedLength, length)));
                }
            })));

                // Retornar verdadero si no hay errores, falso si hay errores
            // Retornar el mismo árbol para permitir encadenamiento
            return tree;
        }
    }
}
