using NorthWind.DomainValidation.Implementations;
using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Helpers
{
    internal static class PropertySpecificationTreeHelper
    {
        public static List<SpecificationError> Validate<T, TProperty>(
            T entity, 
            PropertySpecificationsTree<T, TProperty> tree,

            // delegado
            Action<List<SpecificationError>, TProperty> validator)
        {
            List<SpecificationError> Errors = [];

            var Value = (TProperty)tree.GetPropertyValue (entity);


            // Errors => para almacenar los valor.
            validator(Errors, Value);

            return Errors;
        }



    }
}
