using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static class SetValidatorExtension
    {

        // validación en colecciones de elementos, para una propiedad de tipo colección,
        // queremos indicarle, que valide cada elemento de la colección utilizando un IDomainSpecification
        // validator.

        // tipo de los elementos de la colección.
        public static PropertySpecificationsTree<T, IEnumerable<TElement>>
            SetValidator<T, TElement>(
            this PropertySpecificationsTree<T, IEnumerable<TElement>> tree,
            // Validador de especificaciones de dominio
            IDomainSpecificationValidator<TElement> validator)
        {
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {
                if(value is IEnumerable<TElement> collection)
                {
                    int i = 0;
                    foreach ( var Item in collection)
                    {
                        var Result = validator.ValidateAsync(Item).Result;

                        if (!Result.IsValid)
                        {
                            foreach(var Error in Result.Errors)
                            {
                                errors.Add(new SpecificationError(
                                    $"{tree.PropertyName}[{i}].{Error.PropertyName}",
                                    Error.ErrorMessage
                                    ));
                            }
                        }
                        i++;
                    }
                }
            })));

            return tree;
        }
    }
}
