namespace NorthWind.DomainValidation.Implementations
{

    // nuestra biblioteca tendra implementaciones predeterminadas
    public class Specification<T>(
        Func<T, IEnumerable<SpecificationError>> validationRule) : ISpecification<T>
    {
        public IEnumerable<SpecificationError> Errors { get; private set; }

        public bool IsSatisfiedBy(T entity)
        {
            // un delegado que pueda ser ejecutado???, un Funct
            var ValidationErros = validationRule(entity);

            if (ValidationErros.Any())
            {
                Errors = ValidationErros;
            }

            return ValidationErros?.Any() != true;
        }
    }
}
