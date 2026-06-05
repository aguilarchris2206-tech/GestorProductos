using System;
using System.Collections.Generic;
using System.Text;
using GestorProductos.Common; //Importamos el namespace para usar la clase Producto y BusinessException
//se va hacer una capa de datos de manera manual, porque aún no se ve las conexiones a bases de datos, entonces se va a simular una base de datos con una lista en memoria, para eso se crea la clase ProductoRepository, que va a ser la encargada de manejar los datos de los productos, esta clase va a tener una lista estática de productos y métodos para agregar, eliminar, modificar y obtener productos.
namespace GestorProductos.DataAcess
{
     public class ProductoRepository
    {
        //Creamos una lista estatica de productos (mala práctica, pero, se usara para fines educativos, para simular una base de datos en memoria)
        private static readonly List<Producto> _datos = new()
        {
            new Producto {Id  = 1, Nombre = "Teclado Mecánico", Precio = 45000, Stock = 15 },
            new Producto{Id = 2, Nombre = "Mouse Inalámbrico", Precio = 18000, Stock = 30 },
            new Producto{Id = 3, Nombre = "Monitor 27/", Precio = 185000, Stock = 8}
        };
        public static int _nextID = 4; //Siguiente identificador en la lista


        //Definiendo un método para obtener todos los productos del inventario ACTIVO
        public List<Producto> ObtenerTodos() => _datos.Where(p => p.Activo).ToList(); //Obtenemos todos los productos activos

        public void Insertar(Producto producto)
        {
            producto.Id = _nextID++; //Asignamos el siguiente ID al producto
            _datos.Add(producto); //agregar el producto en la lista _datos
        }

        public void Actualizar(Producto producto)
        {
            //definimos la variable ex para buscar el producto por su ID, si no se encuentra, se lanza una excepción con un mensaje indicando que el producto no se encontró
            // FistOrDefault devuelve el primer elemento que cumple con la condición o el valor predeterminado si no se encuentra ningún elemento, en este caso, si no se encuentra el producto, se lanza una excepción con un mensaje indicando que el producto no se encontró
            // ?? = Operador de fusión de null, si el resultado de FirstOrDefault es null, se lanza la excepción
            var ex = _datos.FirstOrDefault(p => p.Id == producto.Id) //Buscamos el producto por su ID
            ?? throw new Exception($"Producto ID {producto.Id} no encontrado");


            ex.Nombre = producto.Nombre;//Actualizamos el nombre del producto
            ex.Precio = producto.Precio;//Actualizamos el precio del producto
            ex.Stock = producto.Stock;//Actualizamos el stock del producto
        }

        public void Eliminar(int id) //no se utiliza productom ya que con id es suficiente para identificarlo
        {
            var ex = _datos.FirstOrDefault(p => p.Id == id)
                ?? throw new Exception($"Producto ID {id} no encontrado."); //Si no aparece: lanzamos un exception

            ex.Activo = false; 
        }
    }
}
