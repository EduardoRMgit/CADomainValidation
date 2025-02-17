using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.ValueObjects
{
    public class ValidationResult(IEnumerable<SpecificationError> errors)
    {

        // la función del validador es ver que todas las especificaciones se cumpla, y sino, me vaa devolver los erroes en la conexión de errores.
        // va a ser la colección de cada uno de los errores de cada especificación.

        // la especificación valida algo y cuimple el validation result va a tener la consiladación de todas las posibles .

        // para ayudarme para ver si es valido  o no
        // es sencillo para ver si hay errores o no en Erros

        // null  conditional. => verifica si es nul, si no es nulo, hace el resto, si es null

        // checar esto.
        public bool isValid => errors?.Any() != true;




        public IEnumerable<SpecificationError> Errors => errors;
    }
}
