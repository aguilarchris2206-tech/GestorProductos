using System.Text.Json;
using GestorProductos.Common;
namespace GestorProductos.DataAccess
{
    public class ProductoRepository
    {
        private readonly string _ruta;
        public ProductoRepository()
        {
            //definimos la ruta como el mismo folder donde esta el aplicativo.
            //Es posible definir otra ruta? Si!
            //Utilizamos algo como esto: string ruta = @"C:\MisCarpetas\GestorProductos\productos.json";
            //Siempre y cuando la carpeta exista, de otra manera ocurre una excepcion
            _ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"productos.json");
        }

        //Como ya no tenemos lista, necesitamos decirle al sistema como leer y escribir en la nueva fuente de datos
        //Métodos privados de lectura/escritura
        //Los metodos son privados para que nadie mas pueda accederlos
        private List<Producto> Leer()
        {
            //comprobamos que el archivo existe, si no, retornamos una lista vacia de tipo producto
            if (!File.Exists(_ruta)) return new List<Producto>();
            
            //Si el archivo existe, devolvemos la informacion contenida
            string json = File.ReadAllText(_ruta);

            //Puede pasar que el achivo exista y no tenga data, en ese caso validamos que sea asi y retornamos una lista de tipo producto vacia
            return JsonSerializer.Deserialize<List<Producto>>(json)
                ?? new List<Producto>();
        }
        private void Guardar(List<Producto> productos)
        {
            //Para guardar, utilizamos el serializer con las opciones vistas en la presentacion
            var opciones = new JsonSerializerOptions {WriteIndented = true};

            string json = JsonSerializer.Serialize(productos, opciones);

            //Y escribimos el archivo en la ruta establecida
            File.WriteAllText(_ruta, json);
        }


        //Métodos públicos (misma firma que antes)
        public List<Producto> ObtenerTodos()
        {
            return Leer().Where(p => p.Activo).ToList();
        }
        public void Insertar(Producto producto)
        {
            var lista = Leer(); // leemos el contenido del archivo

            //Any() pregunta si existe algun valor en la lista
            // ? si existe un valor:
            //Max() devuelve el valor mas alto y le sumamos 1 para guardar el nuevo id.
            //si no existe ningun valor: asignele 1
            producto.Id = lista.Any() ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(producto);
            Guardar(lista);
        }
        public void Actualizar(Producto producto)
        {
            var lista = Leer(); //leemos el archivo

            //verificamos que exista el dato que queremos modificar
            var existente = lista.FirstOrDefault(p => p.Id == producto.Id)
                ?? throw new Exception( $"Producto ID {producto.Id} no encontrado"); // si es nulo, devolvemos un msg de error
            existente.Nombre = producto.Nombre;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;
            Guardar(lista); //guardamos la lista modificada en el archivo
        }
        public void Eliminar(int id)
        {
            var lista = Leer(); //leemos el archivo

            //verificar que el producto existe
            var existente = lista.FirstOrDefault(p => p.Id == id)
                ?? throw new Exception($"Producto ID {id} no encontrado"); //si null = no existe, retornar error
            existente.Activo = false; //si existe, lo volvemos inactivo (activo = false)
            Guardar(lista); //guardamos el archivo
        }
    }
}