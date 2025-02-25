namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static class EqualExtension
    {
        public static PropertySpecificationsTree<T, TProperty> Equal<T, TProperty>(
            this PropertySpecificationsTree<T, TProperty> tree,

            // debe de ser del mismo tipo de la propiedad
            TProperty comparisonValue,

            // qué está pasando aquí?
            string errorMessage = default) where TProperty : IComparable<TProperty>
        {
            AddSpecification(tree, (entity) => comparisonValue, errorMessage);

            return tree;
        }

        public static PropertySpecificationsTree<T, TProperty> Equal<T, TProperty>(
            this PropertySpecificationsTree<T, TProperty> tree,
            // debe de ser del mismo tipo de la propiedad
            Expression<Func<T, TProperty>> comparisonProperty,
            // qué está pasando aquí?
            string errorMessage = default) where TProperty : IComparable<TProperty>
        {
            AddSpecification(tree, (entity) => comparisonProperty.Compile()(entity),
                errorMessage);
            return tree;
        }

        // este método te pide el valor con el que se va a comprar, sin importar si es el valor el constante o de la propiedad
        static void AddSpecification<T, TProperty>(
            PropertySpecificationsTree<T, TProperty> tree,
            Func<T, TProperty> getComparisonValue, 
            string errorMessage
            ) where TProperty : IComparable<TProperty>
        {
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {
                TProperty comparisonValue = getComparisonValue(entity);
                bool AddError;

                if (value == null && comparisonValue == null)
                {
                    AddError = false; // SON IGUALES
                }
                else if(value == null || comparisonValue == null)
                {
                    AddError = true;
                }
                else
                {
                    // si esto se cumple, retornamos TPropertyValue, sino 
                    TProperty ComparableValue = value is TProperty TPropertyValue
                    ? TPropertyValue
                    : (TProperty) Convert.ChangeType(value, typeof(TProperty));

                    AddError = ComparableValue.CompareTo(comparisonValue) != 0;
                }

                if (AddError)
                {
                    errors.Add(new SpecificationError(tree.PropertyName,
                        errorMessage ?? ErrorMessages.Equal));
                }

            })));
        }

    }
}
