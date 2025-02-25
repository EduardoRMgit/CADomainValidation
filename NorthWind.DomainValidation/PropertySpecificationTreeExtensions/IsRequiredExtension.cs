namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static class IsRequiredExtension
    {
        // (o => o.Customer.ID).IsRequired("es requerido").HasMaxLength("encadenaciones").
        public static PropertySpecificationsTree<T, TProperty> IsRequired<T, TProperty>(
            this PropertySpecificationsTree<T, TProperty> tree,
            string errorMessage = default)
        {
            // Agregar una nueva especificación a la lista de especificaciones del árbol
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {
                // Validar si es nulo o string vacío
                if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                {
                    errors.Add(new SpecificationError(tree.PropertyName,
                        errorMessage ?? ErrorMessages.IsRequired));
                }
            })));

            // Retornar verdadero si no hay errores, falso si hay errores
            // Retornar el mismo árbol para permitir encadenamiento
            return tree;
        }
    }
}
