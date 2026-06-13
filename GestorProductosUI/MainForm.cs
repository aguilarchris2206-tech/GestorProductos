using GestorProductos.Business;
using GestorProductos.Common;

namespace GestorProductosUI
{
    public partial class MainForm : Form
    {
        private readonly ProductoService _service; // Instanciamos un nuevo objeto de tipo producto service (Capa business)

        public MainForm() // Constructor
        {
            InitializeComponent();
            _service = new ProductoService(); // instanciamos la clase para poder acceder a todos sus metodos
            panelInput.GuardarSolicitado += PanelInput_GuardarSolicitado; // Llamada al evento personalizado del input panel
        }

        private void MainForm_Load(object sender, EventArgs e) // Acciones a realizar cuando el formulario carga
        {
            CargarGrid(); // Llenar el datagrid con nuestra lista de productos predefinida
        }

        private void CargarGrid() // llenar el datagrid con los datos de nuestra lista de productos
        {
            dgvProductos.DataSource = null; // limpiamos el datasource del datagrid (fuente de datos)
            dgvProductos.DataSource = _service.ObtenerTodos(); // datasource = lista en dataAccess.
            lblStatus.Text = $"Total: {dgvProductos.Rows.Count} productos!"; // Actualizamos el estado (el numero de elementos en la lista) para el usuario
        }

        private void PanelInput_GuardarSolicitado(object? sender, Producto p) // Utilizacion del evento personalizado en panel input
        {
            try
            { //intentamos
                _service.Guardar(p); //guardar el producto nuevo/modificado
                CargarGrid(); // Refrescamos el grid para que muestre los datos nuevos
                lblStatus.Text = p.Id == 0 ? "Producto Creado con Exito!" : "Producto Actualizado Satisfactoriamente!"; //desplegamos el estado actual al usuario
            }
            catch (BusinessException ex) //Capturamos exception logica de la capa business
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); //mostramos un cuadro de dialogo con el texto del error
                lblStatus.Text = $"Error: {ex.CodigoError}"; // mostramos el codigo de error en nuestra etiqueta de estado
            }
            catch (Exception ex)
            {
                // Capturamos otro tipo de excepcion no esperada
                MessageBox.Show("Ocurrió un error inesperado", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; //Si lo que damos dobleclick es 0 (encabezado del dgv) retornamos sin hacer nada.

            //Casteamos la fila seleccionada en dgv a tipo producto.
            var p = (Producto)dgvProductos.Rows[e.RowIndex].DataBoundItem;

            panelInput.CargarProducto(p); // Cargamos la informacion del producto en nuestros campos de texto
            lblStatus.Text = $"Editando: {p.Nombre}"; // actualizamos el estado de la operacion actual en nuestra etiqueta de estado

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return; // en caso que seleccionemos una celda sin datos (index 0)

            var p = (Producto)dgvProductos.CurrentRow.DataBoundItem; //Tomamos toda la celda de datos y la convertimos a tipo producto
            
            //verificamos el resultado de la eleccion del message box
            if (MessageBox.Show($"Eliminar \"{p.Nombre}\"?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _service.Eliminar(p.Id); // Eliminar el producto con el ID proporcionado
                CargarGrid(); // refrescar el dgv
                lblStatus.Text = "Producto Eliminado con Exito!"; //actualizar el estado de la operacion en el lbl
            }
        }
    }
}
