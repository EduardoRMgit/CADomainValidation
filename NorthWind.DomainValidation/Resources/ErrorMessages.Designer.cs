﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NorthWind.DomainValidation.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NorthWind.DomainValidation.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El formato de correo electrónico es inválido..
        /// </summary>
        public static string EmailAddress {
            get {
                return ResourceManager.GetString("EmailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Los valores no son iguales..
        /// </summary>
        public static string Equal {
            get {
                return ResourceManager.GetString("Equal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El dato debe ser mayor a {0}..
        /// </summary>
        public static string GreaterThan {
            get {
                return ResourceManager.GetString("GreaterThan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El dato debe ser mayor o igual a {0}..
        /// </summary>
        public static string GreaterThanOrEqual {
            get {
                return ResourceManager.GetString("GreaterThanOrEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Se requieren {0} caracteres..
        /// </summary>
        public static string HasFixedLength {
            get {
                return ResourceManager.GetString("HasFixedLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La longitud máxima es de {0} caracteres. .
        /// </summary>
        public static string HasMaxLength {
            get {
                return ResourceManager.GetString("HasMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La longitud mínima es de {0} caracteres..
        /// </summary>
        public static string HasMinLength {
            get {
                return ResourceManager.GetString("HasMinLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Este dato es requerido..
        /// </summary>
        public static string IsRequired {
            get {
                return ResourceManager.GetString("IsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Valor requerido..
        /// </summary>
        public static string NotEmpty {
            get {
                return ResourceManager.GetString("NotEmpty", resourceCulture);
            }
        }
    }
}
