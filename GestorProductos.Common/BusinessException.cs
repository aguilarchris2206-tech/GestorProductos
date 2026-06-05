using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace GestorProductos.Common
{
    public class BusinessException : Exception
    {
        public StringDictionary CodigoError { get; } //No se necesita el SET porque queremos que sea solo de lectura

        //constructor
        public BusinessException(string mensaje, string codigoError) : base(mensaje)
        {
            CodigoError = new StringDictionary();
            CodigoError.Add("Codigo", codigoError);
        }

    }
}
