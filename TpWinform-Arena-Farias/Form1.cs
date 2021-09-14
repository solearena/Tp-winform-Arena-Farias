using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TpWinform_Arena_Farias
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
              
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ArticuloService service = new ArticuloService();
            listaArticulo = service.listar();
            dgvArticulo.DataSource = service.listar();
        }
    }
}
