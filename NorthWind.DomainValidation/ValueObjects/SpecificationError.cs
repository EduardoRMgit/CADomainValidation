using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.ValueObjects
{
    // como quiero que me diga el error, con la propiedad y el error


    public class SpecificationError(string propertyName, string errorMessage)
    {
        public string PropertyName => propertyName;
        public string ErrorMessage => errorMessage;
    }
}
