using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using dominio;
using negocio;

namespace TpWinform_Arena_Farias
{
    public partial class frmAgregar : Form
    {
        public frmAgregar()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar2_Click(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            ArticuloService negocio = new ArticuloService();

            try
            {
                art.CodigoArticulo = txtCodigo.Text;
                art.Nombre = txtNombre.Text;
                art.Descripcion = txtDescripcion.Text;
                art.Precio = decimal.Parse(txtPrecio.Text);
                art.DescripcionMarca = (Marca)cboMarca.SelectedItem;
                art.DescripcionCategoria = (Categoria)cboCategoria.SelectedItem;
                negocio.agregar(art);
                MessageBox.Show("Agregado Excitosamente");
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            MarcaService marcaService = new MarcaService();
            CategoriaService categoriaService = new CategoriaService();
            try
            {
                cboMarca.DataSource = marcaService.listar();
                cboCategoria.DataSource = categoriaService.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
