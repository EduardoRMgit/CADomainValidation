using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Interfaces
{
    // validador de especificaciones de dominio.
    // voy a validar entidades ahi nanita
    public interface DomainSpecificationValidator<T>
    {
        Task<ValidationResult> ValidateAsync(T entity);
    }

}
