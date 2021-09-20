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
    public partial class frmBuscar : Form
    {
        private Articulo articulo = null;
        private List<Articulo> listaArticulo;
        public frmBuscar()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cargar(int id)
        {
            ArticuloService service = new ArticuloService();
            try
            {
                listaArticulo = service.listar();
                int i = 0;
                while( id != listaArticulo[i].Id)
                {
                    i++;
                }
                articulo.Nombre = listaArticulo[i].Nombre;
                articulo.Descripcion = listaArticulo[i].Descripcion;
                articulo.Precio = listaArticulo[i].Precio;
                articulo.ImagenUrl = listaArticulo[i].ImagenUrl;
                articulo.DescripcionMarca = listaArticulo[i].DescripcionMarca;
                articulo.DescripcionCategoria = listaArticulo[i].DescripcionCategoria;
                articulo.CodigoArticulo = listaArticulo[i].CodigoArticulo;
                i = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            articulo = new Articulo();
            articulo.Id = int.Parse(txtCodigo.Text);
            ArticuloService negocio = new ArticuloService();
            cargar(articulo.Id);
            //negocio.buscar(articulo.Id);
            FormDetalle detalle = new FormDetalle(articulo);
            detalle.ShowDialog();
        }
    }
}
