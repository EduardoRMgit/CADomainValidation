namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static class HasMinLengthExtensions
    {
        public static PropertySpecificationsTree<T, string> HasMinLength<T>(
            this PropertySpecificationsTree<T, string> tree,
            int minlength,
            string errorMessage = default)
        {
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {
                if (value != null && value.Length < minlength)
                {
                    errors.Add(new SpecificationError(tree.PropertyName,
                        errorMessage ?? string.Format(ErrorMessages.HasMinLength, minlength)));
                }
            })));

            return tree;
        }
    }
}
