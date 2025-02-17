using NorthWind.DomainValidation.Interfaces;
using NorthWind.DomainValidation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Implementations
{
    // las clases abstract es para que las tengas que implementar, y no las puedas instanciar.
    public abstract class DomainSpecificationBase<T> : IDomainSpecification<T>
    {
        public bool EvaluateOnlyIfNoPreviousErrors { get; protected set; } = false;

        public bool stopOnFirstError { get; protected set; } = false;

        public IEnumerable<SpecificationError> Errors { get; protected set; }

        public Task<bool> ValidateAsync(T entity)
        {
            // DEBE TENER LA LISTA DE ERRORES
            throw new NotImplementedException();
        }

        // Property(c => C.CustomerId).IsRequired();
        protected PropertySpecificationsTree<T, TProperty> Property<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            bool stopInFirstError = false)
        {
            var Tree = new PropertySpecificationsTree<T, TProperty>(
                propertyExpression, stopInFirstError);

            // IMPLEMENTAR 

            return Tree;
        }

    }
}
