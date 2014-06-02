using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comercial_Solutions.Clases;
using Comercial_Solutions.Forms.Principal;
using i3nRiqJSON;

namespace Comercial_Solutions.Forms.Areas.Ventas
{
    public partial class frm_factura : Form
    {
        int X = 0;
        int Y = 0;
        i3nRiqJson db = new i3nRiqJson();
        

        public frm_factura()
        {
            X = Propp.X;
            Y = Propp.Y;
            InitializeComponent();
           
        }

        private void frm_factura_Load(object sender, EventArgs e)
        {
            this.Size = new Size(X, Y);
            this.Location = new Point(250, 56);
            Console.WriteLine(Propp.nombre + " " + Propp.apellido);
            
            CargarInformacion();
        }
        public void CargarInformacion() {
            txtPRUEBA.Text = Propp.IdV + " - " + Propp.nombre + " " + Propp.apellido;
            string queryest = "select tx_establecimiento,cod_establecimiento from tbm_establecimiento";
          cmb_establecimiento.DataSource=(db.consulta_ComboBox(queryest));
          cmb_establecimiento.DisplayMember = "tx_establecimiento";
          cmb_establecimiento.ValueMember = "cod_establecimiento";
          cmb_establecimiento.SelectedIndex = 0;

            string queryfact="select (MAX(no_factura))as no_fact from tbm_factura";
             System.Collections.ArrayList array = db.consultar(queryfact);

             int c = 0;
                foreach (Dictionary<string, string> dic in array){

                    c = (int.Parse(dic["no_fact"]));
                
                }

                lblcorrelativo.Text = ("Factura No:  F - 0"+c);

            //
                string queryEst = "select DISTINCT(tbm_producto_finalizado_idtbm_producto_finalizado),tx_nombre from tbm_almacen,tbt_inventario,tbm_producto_finalizado where tbm_almacen.tbm_establecimiento_cod_establecimiento='" + cmb_establecimiento.SelectedValue.ToString() + "' AND tbt_inventario.tbm_almacen_idtbm_bodega=tbm_almacen.idtbm_bodega AND  tbm_producto_finalizado.idtbm_producto_finalizado	=tbm_producto_finalizado_idtbm_producto_finalizado";
             

           cmb_producto.DataSource=(db.consulta_ComboBox(queryEst));
           cmb_producto.DisplayMember = "tx_nombre";
           cmb_producto.ValueMember = "tbm_producto_finalizado_idtbm_producto_finalizado";
          
            //}
            //catch (Exception d) { }
            //    
        }

        public void ARMAR(string XX) {
            System.Collections.ArrayList array6 = db.consultar("select tx_nombre from tbm_producto_finalizado where idtbm_producto_finalizado=" + XX+ ";");

            foreach (Dictionary<string, string> dic2 in array6)
            {
                Console.WriteLine(dic2["tx_nombre"]);
            }


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        }

    }

