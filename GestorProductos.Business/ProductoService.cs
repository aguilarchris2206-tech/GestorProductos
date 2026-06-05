using System;
using System.Collections.Generic;
using System.Text;
using GestorProductos.Common;
using GestorProductos.DataAcess;

namespace GestorProductos.Business
{
    internal class ProductoService
    {
        private readonly ProductoRepository _repo; //Variable privada de solo lectura, de típo producto repository

        public ProductoService() { _repo = new ProductoRepository(); } //Constructor -- llamna a todos los metodos que existian en la clase padre (Producto Repository)
        public List<Producto> ObtenerTodos() => _repo.ObtenerTodos(); 

        public void Guardar(Producto producto)
        {
            //Validaciones
            //Evaluamos diferentes atributos de nuestro parametro para desplegar errores en caso de datos erroneos

            //1. Si el nombre esta en blanco.
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new BusinessException("El nombre del producto es requerido", "PROD_NOMBRE:REQ");

            //2. Si el nombre es muy corto 
            if (producto.Nombre.Length < 3)
                throw new BusinessException("El nombre del producto debe tener al menos 3 caracteres", "PROD_NOMBRE_CORTO");

            //3. Si el precio es menor a cero
            if (producto.Precio <= 0)
                throw new BusinessException("El precio del producto es inválido", "PROD_PRECIO_INV");

            //4. Si el Stock es negativo 
            if (producto.Stock < 0)
                throw new BusinessException("El stock del producto no puede ser negativo", "PROD_STOCK_NEG");

            //Validamos que el id exista, si no, es un producto nuevo y se llama a insertar.
            if (producto.Id == 0) _repo.Insertar(producto);
            else _repo.Actualizar(producto);
        }


        public void Eliminar(int id) => _repo.Eliminar(id); //Eliminacion

    }
}
