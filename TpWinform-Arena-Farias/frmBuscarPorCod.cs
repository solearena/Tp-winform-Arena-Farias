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
    public partial class frmBuscarPorCod : Form
    {
        private Articulo articulo = null;
        private List<Articulo> listaArticulo;
        public frmBuscarPorCod()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cargar(string cod)
        {
            ArticuloService service = new ArticuloService();
            try
            {
                listaArticulo = service.listar();
                int i = 0;
                while (cod != listaArticulo[i].CodigoArticulo)
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
            catch (Exception)
            {
                MessageBox.Show("NO EXISTE EL CÓDIGO A BUSCAR");
            }
        }
        private bool existe(string cod)
        {
            ArticuloService service = new ArticuloService();

            listaArticulo = service.listar();
            int i = 0;
            while (cod != listaArticulo[i].CodigoArticulo)
            {
                i++;
            }
            if (listaArticulo[i].CodigoArticulo == cod)
            {
                return true;
            }
            return false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            articulo = new Articulo();
            bool chequear;
            articulo.CodigoArticulo = txtCodigo.Text;
            cargar(articulo.CodigoArticulo);
            chequear = existe(articulo.CodigoArticulo);
            if (chequear == true)
            {
                FormDetalle detalle = new FormDetalle(articulo);
                detalle.ShowDialog();
            }
            else
            {
                Close();
            }
        }
    }
}
