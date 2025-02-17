using NorthWind.DomainValidation.Extensions;
using NorthWind.DomainValidation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Implementations
{

    // Necesito que me pasen la propiedad, no solo el nombre => CreateOrder.CustomerID

    // Lo necesito la expresión donde pueda representar la propiedad misma.
    // una expresión es una sintaxis que me permite definir operandos y operadores: => a + b, 5 etc.
    // En este caso se envuelve para obtener el la entidad y la propiedad.

    // Es una expresión tal que => envuelva a un delegado, que cuando yo lo invoque, me devuelva la propiedad,
    // ademas quiero que me pases un booleano con el stopOnFirstError, 


    // en el delegado nos va a entregar la entidad, y nos va a devolver la propiedad.
    // Aquí extraemos el delegado que nos retorna la propiedad.

    // new PropertySpecificationsTree<CreateOrder, int>(c => c.CustomerID);

    // necesitamos la expresión, pero a la expresión le ando extreyando el delegado.


    // la expresión también me sirve para obtener el nombre de la variable.

    public class PropertySpecificationsTree<T, TProperty>(
        Expression<Func<T,TProperty>> propertyExpression,
        bool stopInFirstError = false) : 
        IPropertySpecificationsTree<T>
    {

        // Este va a tener el contenido de la expresión, llamar al delegado pasandote a la entidad.
        readonly Func<T, TProperty> PropertyExpressionDelegate =
            propertyExpression.Compile();

        // implementar getPropertyName

        // nombre de la propiedad, obtenemos el nombre del a propiedad, de la expresión, y el valor
        public string PropertyName { get; } = propertyExpression.GetPropertyName();

        // lista de especificaciones
        public List<ISpecification<T>> Specifications { get; } = [];

        public bool StopInFirstError => stopInFirstError;

        // Aquí ejecutamos el delegado que nos va a retornar la propiedad.
        // obtenemos el valor del delegado.
        public object GetPropertyValue(T entity) => PropertyExpressionDelegate(entity);

    }
}
