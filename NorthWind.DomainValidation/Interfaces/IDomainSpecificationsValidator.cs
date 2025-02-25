

namespace NorthWind.DomainValidation.Interfaces
{
    // validador de especificaciones de dominio.
    // voy a validar entidades ahi nanita
    public interface IDomainSpecificationValidator<T>
    {
        Task<ValidationResult> ValidateAsync(T entity);
    }

}
