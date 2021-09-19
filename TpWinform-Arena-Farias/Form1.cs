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
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            ArticuloService service = new ArticuloService();
            try
            {
                listaArticulo = service.listar();
                dgvArticulo.DataSource = listaArticulo;
                dgvArticulo.Columns["ImagenUrl"].Visible = false;
                dgvArticulo.Columns["Id"].Visible = false;
                dgvArticulo.Columns["Descripcion"].Visible = false;
                cargarImagen(listaArticulo[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("http://www.carsaludable.com.ar/wp-content/uploads/2014/03/default-placeholder.png");
            }
        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar alta = new frmAgregar();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
                Articulo seleccionado = new Articulo();
                
            try
            {
                seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem; //arreglado con el System.NullReferenceException
                frmAgregar modificar = new frmAgregar(seleccionado);
                modificar.ShowDialog();
                cargar();
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("ERROR: Listar articulos");
            }

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {

            try
            {
                Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem; //arreglado con el System.NullReferenceException
                FormDetalle detalle = new FormDetalle(seleccionado);
                detalle.ShowDialog();
                cargar();
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("ERROR");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                frmBuscar detalle = new frmBuscar();
                detalle.ShowDialog();
                cargar();
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("ERROR");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
