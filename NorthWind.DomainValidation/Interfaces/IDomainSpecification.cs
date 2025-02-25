namespace NorthWind.DomainValidation.Interfaces
{
    public interface IDomainSpecification<T>
    {
        // evaluar only if no previoc
        // si esto es true, cuando se valida una validación y si hay error. Detener 


        // verificar si una especificación distinta tuvo o no tuvo errores.
        bool EvaluateOnlyIfNoPreviousErrors { get; } // decide si sigue evaluando cuando una especificacion independiente falla


        // queremos que en esta especificación, se detenga en el primero error.
        // cumplió o si cumplió, si está en true, ya no continuo, cada que haya un error, se va a verificar
        // el valor de esta propiedad, si esta en true, se termina la validación, y si no, continua y continua.

        // ES A NIVEL DE ESPECIFICACIÓN.
        bool StopOnFirstError { get; } // fallar o deternar si una regla falla


        IEnumerable<SpecificationError> Errors { get; } // si encuentra errores ahí los devuelve


        Task<bool> ValidateAsync(T entity); // metodo para realizar la validación.

        // 5 caracteres, otra especificacion que el cliente debe existir



        // la especificación va a decir solo evaluame si no hay valores previos, pero ya hay un error.

        // en una especificacion, 
    }
}

// este es más avanzado
// esta evalua todas las reglas de las especificaciones, ship address, postal code, y weas así.