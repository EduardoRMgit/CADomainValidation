namespace NorthWind.DomainValidation.PropertySpecificationTreeExtensions
{
    public static partial class EmailAddressExtension
    {
        /* Parte local 
         *  [a-zA-Z0-9]+ => Permite a-z, A-Z y números 0-9, al menos debe de haber un carácter del grupo.
         *  ([._%+-][a-zA-Z0-9]+)* => 
         *  [._%+-]: Permite el grupo de carácteres especiales
         *  [a-zA-Z0-9]+: Después de un carácter especial, debe almenos haber un alfanúmerico
         *  *: se repite 0 o más veces.
         *  @ 
         *  Dominio
         *  [[a-zA-Z0-9-]+ => Permite a-z, A-Z, 0-9 y -
         *  \.
         *  Terminación
         *  [a-zA-z]: Letras 
         *  {2, }: Indica que al menos debe haber 2 letras.
         */

        // en Net 7, le han inventado un atributo llamado GeneratedRegex, donde darle la expresión regular
        // para el compilador compile la Regex para usarla más rápida en tiempo de ejecyución. esta variable se le debe 
        // asignar a un método
        [GeneratedRegex(@"^[a-zA-Z0-9]+([._%+-][a-zA-Z0-9]+)*@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$")]

        private static partial Regex EmaiRegex();


        public static PropertySpecificationsTree<T, string> EmailAdressValidation<T>(
            this PropertySpecificationsTree<T, string> tree,
            string errorMessage = default)
        {
            tree.Specifications.Add(new Specification<T>(entity =>
            PropertySpecificationTreeHelper.Validate(entity, tree, (errors, value) =>
            {
                if (string.IsNullOrWhiteSpace(value) || !EmaiRegex().IsMatch(value))
                {
                    errors.Add(new SpecificationError(tree.PropertyName,
                        errorMessage ?? ErrorMessages.EmailAddress));
                }
            })));


            return tree;
        }

    }
}
