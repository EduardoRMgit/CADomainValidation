
namespace NorthWind.DomainValidation.Implementations
{
    // el programa que consuma este framework, es crear especificaciones e inyectarlas.


    // Validación de especificaciones del dominio.
    public class DomainSpecificationValidator<T>(
        IEnumerable<IDomainSpecification<T>> specifications) 
        : IDomainSpecificationValidator<T>
    {

        public async Task<ValidationResult> ValidateAsync(T entity)
        {
            List<SpecificationError> Errors = [];


            // Aquellas especificaciones que se deben evaluar SIEMPRE SIEMPRE SIEMPRE;
            var UnconditionalSpecifications = specifications.Where(
                spec => !spec.EvaluateOnlyIfNoPreviousErrors);

            // Especificaciones con que tienen condición
            var ConditionalSpecifications = specifications.Where(
                spec => spec.EvaluateOnlyIfNoPreviousErrors);


            // Agregar errores en las incondicionales
            foreach(var Specification in UnconditionalSpecifications)
            {
                // si no validaron o arrojó un error
                if(!await Specification.ValidateAsync(entity))
                {
                    // Recorren los errores de cada especificación incondicional.
                    Errors.AddRange(Specification.Errors);
                }
            }

            // Agregar errores para las condicionales.

            // ENUMERAR CADA ELEMENTO DE ESA COLECCIÓN
            var Enumerator = ConditionalSpecifications.GetEnumerator();
            bool isValid = Errors.Count == 0; // no hay error y puedo hacer el recorrido de las incondicionales.

            while(Enumerator.MoveNext() && isValid)
            {
                // va a esperar a la validación del elemento actual
                isValid = await Enumerator.Current.ValidateAsync(entity);
                if (!isValid)
                {
                    Errors.AddRange(Enumerator.Current.Errors);
                }
            }

            return new ValidationResult(Errors);

        }

    }
}
