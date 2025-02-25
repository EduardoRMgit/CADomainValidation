namespace NorthWind.DomainValidation.Interfaces
{
    // una entidad cumple la validación de una entidad o no

    // el implementador recibe cualquier tipo de entidad
    // decidir si una entidad satisface uan validación.
    // y luego preguntamos si tiene la validación 
    public interface ISpecification<T>
    {
        // que me retorne la lista de errores generados.
        IEnumerable<SpecificationError> Errors { get; }


        // devuelve si o no, si cumple el requerimiento
        bool IsSatisfiedBy(T entity);

        // max length y así
    }
}
// aquí van todas las especificaciones