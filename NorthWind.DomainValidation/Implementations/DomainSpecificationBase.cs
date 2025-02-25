namespace NorthWind.DomainValidation.Implementations
{
    // las clases abstract es para que las tengas que implementar, y no las puedas instanciar.
    public abstract class DomainSpecificationBase<T> : IDomainSpecification<T>
    {
        public DomainSpecificationBase(bool evaluateOnlyIfNoPreviousErrors = false)
        {
            EvaluateOnlyIfNoPreviousErrors = evaluateOnlyIfNoPreviousErrors;
        }

        public bool EvaluateOnlyIfNoPreviousErrors { get; }

        public bool StopOnFirstError { get; protected set; } = false;

        public IEnumerable<SpecificationError> Errors { get; protected set; }

        public async Task<bool> ValidateAsync(T entity)
        {
            // DEBE TENER LA LISTA DE ERRORES
            Errors = await ValidateSpecificationAsync(entity);

            return Errors.Count() == 0;
        }

        // la clase que hereda, es la única que pueda implementar.
        // esta es donde 
        protected virtual Task<List<SpecificationError>> ValidateSpecificationAsync(T entity)
        {
            // valida las especificaciones, pero no permite contra db o algo así, eso es otro asunto.


            // for each => trees
                // for each => specifications

            // tenemos que preguntar si stop on first error es true, muerte todo, 
            List<SpecificationError> SpecificationErrors = [];
            // enumerar arboles, next next y voy 
            var TreesEnumerator = PropertySpecificationsForest.GetEnumerator();
            bool ContinueValidatingTrees = true;
            // validar arboles hasta que se indique lo contrario
            // cuando no haya arboles, no se va a poder mover, mientras te puedas mover al siguiente y la bandera te lo permite
            while(TreesEnumerator.MoveNext() && ContinueValidatingTrees)
            {
                // enumerador para las especificaciones del arbol
                // ENUMERADOR DE ESPECIFICACIONES DEL ÁRBOL ACTUAL
                var TreeSpecificationEnumerator = TreesEnumerator.Current.Specifications.GetEnumerator();
                bool ContinueValidatingTreeSpecifications = true;


                // árbolito, detente cuando te dijeron que te detengas en el primer error del árbol, enciende tu bandera de que ya terminaste
                while(TreeSpecificationEnumerator.MoveNext() && ContinueValidatingTreeSpecifications)
                {
                    // DEL arbol actual, si es satisfecho, si la entidad satisface las reglas de validación
                    if(!TreeSpecificationEnumerator.Current.IsSatisfiedBy(entity))
                    {
                        // LOS ERRORES QUE NO CUMPLIO, SE AGREGAN A LA COLECCIÓN DE ERRORES, si un arbol generó 3 errores.
                        SpecificationErrors.AddRange(TreeSpecificationEnumerator.Current.Errors);


                        // detenemos en el árbol de especificaciones, detener en el primer error del arbol
                        // si no se cumplió lo que se quería de esta propiedad.
                        if (TreesEnumerator.Current.StopInFirstError)
                            ContinueValidatingTreeSpecifications = false;
                    }
                }

                // detener cuando haya error y me tenga que detener en el primer error.
                // vas a validar el otro arbolito dependiendo de la bandera.
                if (SpecificationErrors.Count != 0 && StopOnFirstError)
                    ContinueValidatingTrees = false;



            }
            return Task.FromResult(SpecificationErrors);

        }

        List<IPropertySpecificationsTree<T>> PropertySpecificationsForest = [];


        // Property(c => C.CustomerId).IsRequired();
        protected PropertySpecificationsTree<T, TProperty> Property<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            bool stopInFirstError = false)
        {

            // toda la cadena de validación, para una sola propiedad.
            var Tree = new PropertySpecificationsTree<T, TProperty>(
                propertyExpression, stopInFirstError);

            // quiero agregar el árbol de propiedades, pero ahora quiero para address, etc, el bosque es la combinación de árboles de specifications.
            PropertySpecificationsForest.Add(Tree); 

            return Tree;
        }

    }
}
