using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDomainSpecificationsValidator(this IServiceCollection services)
        {

            // Quiere el tipo.
            // Cómo obtengo el tipo de un 
            services.TryAddScoped(typeof(IDomainSpecificationValidator<>),
                typeof(DomainSpecificationValidator<>));

            // Intengar agregar los servicios que encuentre de ese tipo
            // Normalmente, al inyector de dependencias, si ut inventas se va a registrar 2 veces.
            // Se va a duplicar, 
            // ¿Cuál es la diferencia 

            return services;
        }
    }
}
