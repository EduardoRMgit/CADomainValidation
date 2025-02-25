namespace NorthWind.DomainValidation.Interfaces
{
    // VA A TENER LAS ESPECIFICACIONES DEL A PROPIEDAD
    // arbol de especificaciones de la propiedad.
    // Propiedad de Create Order, detailDTO, puede ser cualquier entidad.
    public interface IPropertySpecificationsTree<T>
    {
        string PropertyName { get; }
        List<ISpecification<T>> Specifications { get; }
        bool StopInFirstError { get; }

        // cadenas, enteros, siempre que se pueda hay que evitarlo
        object GetPropertyValue(T entity); // devolver el valor de la propiedad, string, defenir el papa de todas las clases. puede almacenar cualquier cosa, 

    }

    // guardar las especificaicones de cada propiedad.
}
