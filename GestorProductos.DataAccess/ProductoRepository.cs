using System;
using System.Collections.Generic;
using System.Text;
using GestorProductos.Common;

namespace GestorProductos.DataAccess
{
    public class ProductoRepository
    {
        //Creamos una lista estatica de productos
        private static readonly List<Producto> _datos = new()
        {
            new Producto{Id=1, Nombre="Teclado mecánico", Precio=45000, Stock=15}, // instanciar objeto de tipo producto
            new Producto{Id =2, Nombre="Mouse Inhalámbrico", Precio=18000, Stock=30},
            new Producto{Id=3, Nombre="Monitor 27\"", Precio=185000, Stock=8},
        };

        public static int _nextId = 4; //Siguiente identificador en la lista
        
        //CRUD

        // Definiendo un metodo para obtener todos los productos con estado activo en el repositorio
        public List<Producto> ObtenerTodos() => _datos.Where(p => p.Activo).ToList();

        public void Insertar(Producto producto) // Metodo para insertar productos nuevos
        {
            producto.Id = _nextId++; // asignar el id del producto
            _datos.Add(producto); // agregar el producto en nuestra lista _datos
        }

        public void Actualizar(Producto producto)
        {
            // Definimos la variable ex para almacenar el producto de la lista datos que nos haga match con el id del producto que le pasamos por parametro
            // FirstOrDefault = metodo para recorrer listas, y buscar elementos por algun parametro o valor
            // ?? = Operador que sirve para verificar si el valor de una variable es null despues de un procedimiento
            var ex = _datos.FirstOrDefault(p => p.Id == producto.Id)
                ?? throw new Exception($"Producto ID {producto.Id} no encontrado!");

            // Asignando valores a cada uno de los atributos del producto ex
            ex.Nombre = producto.Nombre;
            ex.Precio = producto.Precio;
            ex.Stock = producto.Stock;
        }

        public void Eliminar(int id) // Aqui no utilizamos un producto como parametro, ya que con solo el id es suficiente para identificarlo
        {
            var ex = _datos.FirstOrDefault(p => p.Id == id) // Recorremos la lista buscando el match del id del producto
                ?? throw new Exception($"Producto ID {id} no encontrado!"); // Si no aparece: lanzamos exception

            ex.Activo = false; // Si aparece: marcamos el producto como inactivo
        }
    }
}
