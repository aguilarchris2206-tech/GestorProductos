using System;
using System.Collections.Generic;
using System.Text;
using GestorProductos.Common;
using GestorProductos.DataAccess;

namespace GestorProductos.Business
{
    public class ProductoService
    {
        private readonly ProductoRepository _repo; // Variable privada de solo lectura, de tipo producto repository -- instanciacion de clase

        public ProductoService() { _repo = new ProductoRepository(); } // Constructor -- llama a todos los metodos que existan de la clase padre (Producto Repository)
        public List<Producto> ObtenerTodos() => _repo.ObtenerTodos(); // Llamamos al metodo que existe en la clase ProductoRepository -- Lectura

        public void Guardar(Producto producto) // Insercion y Actualizacion
        {
            // Validaciones
            // Evaluamos diferentes atributos de nuestro parametro para desplegar errores en caso de datos erroneos

            //1. Si el nombre esta en blanco
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new BusinessException("El nombre del producto es requerido", "PROD_NOMBRE_REQ");

            //2. Si el nombre es muy corto
            if (producto.Nombre.Length < 3)
                throw new BusinessException("El nombre debe tener al menos 3 caracteres","PROD_NOMBRE_CORTO");

            //3. Si el precio es menor que cero
            if (producto.Precio <= 0)
                throw new BusinessException("El precio debe ser mayor a cero","PROD_PRECIO_INV");

            //4. Si el stock es negativo
            if (producto.Stock < 0)
                throw new BusinessException("El stock no puede ser negativo","PROD_STOCK_NEG");

            //5. Validamos que el id exista, si no, es un producto nuevo y se llama a insertar
            if (producto.Id == 0) _repo.Insertar(producto);
            else _repo.Actualizar(producto);
        }

        public void Eliminar(int id) => _repo.Eliminar(id); //Eliminacion

    }
}
