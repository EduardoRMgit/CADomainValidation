using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.ValueObjects
{
    public class ValidationResult
    {
        private readonly IEnumerable<SpecificationError> _errors;

        public ValidationResult(IEnumerable<SpecificationError> errors)
        {
            _errors = errors ?? Enumerable.Empty<SpecificationError>(); // Evita null
        }

        public bool IsValid => !_errors.Any(); // Si no hay errores, es válido

        public IEnumerable<SpecificationError> Errors => _errors;
    }
}
