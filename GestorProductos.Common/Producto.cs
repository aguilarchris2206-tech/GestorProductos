using System;
using System.Collections.Generic;
using System.Text;

namespace GestorProductos.Common
{
    public class Producto // Public: para que sea accesible a cualquier proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; } = true;
    }
}
