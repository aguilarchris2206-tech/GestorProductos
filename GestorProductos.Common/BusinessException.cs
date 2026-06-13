using System;
using System.Collections.Generic;
using System.Text;

namespace GestorProductos.Common
{
    public class BusinessException : Exception
    {
        public string CodigoError { get; } // No necesitamos el SET porque queremos que sea solo de lectura

        //constructor
        public BusinessException(string mensaje, string codigoError) : base(mensaje)
        {
            CodigoError = codigoError; //Asignamos el valor de codigoError de parametro a la propiedad de la excepción
        }
    }
}
