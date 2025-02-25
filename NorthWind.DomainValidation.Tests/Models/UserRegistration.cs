using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DomainValidation.Tests.Models
{
    public class UserRegistration
    {
        public const string IsRequiredErrorMessage = 
            "Dato requerido";
        public const string HasMinLengthErrorMessage = 
            "Se requieren al menos 6 caracteres.";
        public const string UpperCaseCharactersAreRequiredErrorMessage =
            "Se requieren caracteres mayúsculas";
        public const string LowerCaseCharactersAreRequiredErrorMessage =
            "Se requieren caracteres minúsculas";
        public const string DigitAreRequiredErrorMessage =
            "Se requieren digitos";
        public const string SymbolsAreRequiredErrorMessage =
            "Se requieren simbolos";
        public const string PassWordConfirmationNotMatchErrorMessage =
            "La confirmación de password no coincide";

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
