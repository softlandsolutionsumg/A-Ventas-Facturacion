using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comercial_Solutions.Forms.Areas.Ventas;
using Comercial_Solutions.Clases;
using i3nRiqJSON;

namespace Comercial_Solutions.Forms.Areas.Ventas
{
    public partial class frm_autorizacionvendedor : Form
    {
        i3nRiqJson db = new i3nRiqJson();
        public frm_autorizacionvendedor()
        {
            InitializeComponent();
        }

        private void frm_autorizacionvendedor_Load(object sender, EventArgs e)
        {

        }

        private void btn_autenticar_Click(object sender, EventArgs e)
        {
            Verificar();
        }

        public void Verificar() {

           
            
           
            string usuario = Properties.Settings.Default.xnomusuario;
            string query = "select * from tbm_vendedor where usuarios_cod_usuario=(select cod_usuario from usuarios where usu_usuario= '" + usuario + "') AND tbm_empleado_idtbm_empleado=(select idtbm_empleado from tbm_empleado where nombre_empleado='" + txt_nombre.Text + "' AND apellido_empleado='" + txt_apellido.Text + "')";
            System.Collections.ArrayList array = db.consultar(query);
            int tam = array.Count;
            if (tam > 0)
            {
                foreach (Dictionary<string, string> dic in array)
                {

                    Propp.IdV=(dic["idtbm_vendedor"]);
                    Console.WriteLine(dic["idtbm_vendedor"]);
                }


                frm_factura x = new frm_factura();

                Propp.nombre = txt_nombre.Text;
                Propp.apellido = txt_apellido.Text;


                x.MdiParent = this.MdiParent;

                try
                {

                    x.Show();
                    // this.Dispose();
                }
                catch (Exception f) { }

            }
            else
            {
                MessageBox.Show("USUARIO invalido" );
                txt_apellido.Text = "";
                txt_nombre.Text = "";
                txt_nombre.Focus();
            }

           

        }
    }
}
