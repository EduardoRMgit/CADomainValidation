using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static class NotEmptyExtension
    {
        public static PropertySpecificationsTree<T, TProperty> NotEmpty<T, TProperty>(
            this PropertySpecificationsTree<T, TProperty> tree,
            string errorMessage = default)
        {
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {

                // El Any, solo viene del IEnumerable, por lo que lo transforma en un INemuerable de collection.
                if (value == null || value is IEnumerable collection &&
                !collection.Cast<object>().Any())
                {
                    errors.Add(new SpecificationError(tree.PropertyName,
                        errorMessage ?? ErrorMessages.NotEmpty));
                }
            })));

            return tree;
        }
    }
}
