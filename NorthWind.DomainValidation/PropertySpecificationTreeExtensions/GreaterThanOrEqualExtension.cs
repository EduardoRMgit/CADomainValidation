
namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static class GreaterThanOrEqualExtension
    {
        public static PropertySpecificationsTree<T, TProperty> GreaterThanOrEqual<T, TProperty>(
            this PropertySpecificationsTree<T, TProperty> tree,
            TProperty comparisonValue,
            // Aquí ponemos un where Tproperty para hacer que solo donde TProperty sea comparable, usa IComparable
            string errorMessage = default) where TProperty : IComparable<TProperty>
        {
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {
                // Viene como objeto así que lo tenemos que almacenar
                var ComparableValue = (TProperty)Convert.ChangeType(value, typeof(TProperty));
                // Con el compareTo:
                //   0  => igual
                // < 0  => menor
                // > 0  => mayor
                if (ComparableValue.CompareTo(comparisonValue) < 0)
                {
                    errors.Add(new SpecificationError(tree.PropertyName,
                    errorMessage ?? string.Format(ErrorMessages.GreaterThanOrEqual, comparisonValue)));
                }



            })));

            return tree;
        }
    }
}
