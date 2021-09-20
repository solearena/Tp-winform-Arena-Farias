using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TpWinform_Arena_Farias
{
    public partial class FormDetalle : Form
    {
        private Articulo articulo = null;
        private List<Articulo> listaArticulo;
        public FormDetalle()
        {
            InitializeComponent();
        }
        public FormDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }
        
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen2.Load(imagen);
            }
            catch (Exception)
            {
                pbxImagen2.Load("http://www.carsaludable.com.ar/wp-content/uploads/2014/03/default-placeholder.png");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormDetalle_Load(object sender, EventArgs e)
        {
            MarcaService marcaService = new MarcaService();
            CategoriaService categoriaService = new CategoriaService();
            
            try
            {
                
                if (articulo != null)
                {
                    txtCodigo.Text = articulo.CodigoArticulo;
                    txtNombre2.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtUrlImagen.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);
                    txtMarca.Text = articulo.DescripcionMarca.Descripcion;
                    txtCategoria.Text = articulo.DescripcionCategoria.Descripcion;
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Close();
            }
        }

        private void pbxImagen2_Click(object sender, EventArgs e)
        {

        }
    }
}
