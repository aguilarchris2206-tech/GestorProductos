using GestorProductos.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GestorProductos.UI
{
    public partial class ProductInputPanel : UserControl
    {
        public event EventHandler<Producto>? GuardarSolicitado; // Declaramos un evento personalizado
        public ProductInputPanel() // Constructor de nuestro user control
        {
            InitializeComponent();
        }

        public void CargarProducto(Producto p) // Cargamos los valores de los atributos de producto en los campos de texto para actualizar/guardar
        {
            txtNombre.Text = p.Nombre;
            txtPrecio.Text = p.Precio.ToString();
            txtStock.Text = p.Stock.ToString();
            txtNombre.Tag = p.Id;
        }

        private void btnGuardar_Click(object sender, EventArgs e) // Definimos el comportamiento del evento click del boton guardar
        {
            lblError.Text = "";
            try
            {
                var p = new Producto
                {
                    // Usamos el operador ternario para comprobar el valor en la propiedad Tag del textbox 
                    Id = txtNombre.Tag is int id ? id : 0, // si es algun entero se deja como tal, si no trae valor, se le asigna 0
                    Nombre = txtNombre.TextoReal, // nombre = valor de texto
                    Precio = decimal.Parse(txtPrecio.TextoReal), // parseamos el valor en el campo de texto a decimal para su almacenamiento
                    Stock = int.Parse(txtStock.TextoReal)
                };
                GuardarSolicitado?.Invoke(this, p); // Llamamos al evento personalizado y le pasamos por parametro el elemento a guardar.
            }
            catch (FormatException) // Capturamos posible exception
            {
                lblError.Text = "Verifique que precio y stock sean numéricos!"; // Actualizamos el texto de la etiqueta para informarle al usuario
            }
        }
        private void ProductInputPanel_Load(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e) // Definimos el comportamiento del boton Limpiar
        {
            // Limpiamos todos los campos de texto
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtNombre.Tag = 0;
            lblError.Text = "";
            // Enfocamos el campo de nombre
            txtNombre.Focus();
        }
    }
}
